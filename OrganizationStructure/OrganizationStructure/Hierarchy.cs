using System.Collections.Generic;

namespace OrganizationStructure
{
    public abstract class Hierarchy
    {
        // Название
        private string Name { get; set; }

        // Список нижестоящих структур
        private List<Hierarchy> Subhierarchy { get; set; }

        // Список элементов на текущем уровне
        private List<object> Elements { get; set; }

        /// <summary>
        /// Метод добавления структуры в иерархию
        /// </summary>
        /// <param name="o1">Добавляемая структура</param>
        /// <param name="o2">Параметр, зависящий от контекста</param>
        /// <param name="director">Еще один параметр, зависящий от контекста</param>
        /// <returns>Созданная структура</returns>
        public abstract object Append(object o1, int o2, bool director);
    }
}
