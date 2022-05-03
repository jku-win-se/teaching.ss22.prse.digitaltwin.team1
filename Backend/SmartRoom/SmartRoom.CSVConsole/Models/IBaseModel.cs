﻿using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CSVConsole.Models
{
    public interface IBaseModel<E> where E : new()
    {
        public E GetEntity();

    }
}
