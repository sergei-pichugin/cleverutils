namespace Counter.Services
{
    public static class CounterService
    {
        // Состояние: 0 - свободен (нет записи), 1 - идет запись
        private static int _writeLockState = 0;
        private static int _count = 0;

        public static int GetCount()
        {            
            while (true)
            {
                // Пытаемся получить разрешение на чтение. 
                // Мы читаем, только если состояние _writeLockState равно 0.
                // Если оно 0, мы атомарно меняем его на 0 (то есть фактически ничего не меняем),
                // но возвращаем старое значение. Если старое значение 1 - значит идет запись.
                if (Interlocked.CompareExchange(ref _writeLockState, 0, 0) == 0)
                {
                    // Чтение безопасно
                    return _count;
                }
                // Если кто-то уже пишет или читает, ждем и пробуем снова (SpinWait)
                Thread.SpinWait(10);
            }
            throw new InvalidOperationException("Чтение невозможно.");
        }

        public static void AddToCount(int value)
        {
            // Пытаемся перевести состояние из 0 в 1 (блокируем для других)
            while (Interlocked.CompareExchange(ref _writeLockState, 1, 0) != 0)
            {
                // Если кто-то уже пишет или читает, ждем и пробуем снова (SpinWait)
                Thread.SpinWait(10);
            }

            try
            {
                // Критическая секция: выполняем запись
                _count = _count + value;
            }
            finally
            {
                // Завершили запись: возвращаем состояние в 0
                Interlocked.Exchange(ref _writeLockState, 0);
            }
        }

    }
}
