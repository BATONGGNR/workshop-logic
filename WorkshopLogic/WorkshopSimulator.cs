namespace WorkshopLogic
{
    public class WorkshopSimulator
    {
        // Расчёт коэффициента загрузки станка
        // workingTime - время работы, downtime - время простоя
        public double CalculateLoadFactor(int workingTime, int downtime)
        {
            if (workingTime + downtime == 0)
                return 0.0;

            return (double)workingTime / (workingTime + downtime);
        }

        // Проверка возможности обработки детали
        // queueSize - текущая очередь, maxCapacity - максимальная вместимость
        public bool CanProcessPart(int queueSize, int maxCapacity)
        {
            return queueSize < maxCapacity;
        }

        // Расчёт среднего времени обработки партии деталей
        // partCount - количество деталей, timePerPart - время на одну деталь
        public int CalculateBatchTime(int partCount, int timePerPart)
        {
            if (partCount <= 0 || timePerPart <= 0)
                throw new ArgumentException("Некорректные параметры");

            return partCount * timePerPart;
        }
    }
}