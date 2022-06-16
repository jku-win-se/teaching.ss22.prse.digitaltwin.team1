using SmartRoom.CommonBase.Core.Contracts;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.TransDataService.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartRoom.TransDataService.Tests
{
    public class StateActionsTest
    {
        [Fact]
        public void Ctor_ValidParam_Ok()
        {
            var res = new StateActions(new Action<IEnumerable<IState>>(TestMethode));

            Assert.NotNull(res);
        }

        [Fact]
        public void Ctor_RunActions_ChangesTimeStamp()
        {
            IState[] states = new IState[] { new BinaryState() };
            var res = new StateActions(new Action<IEnumerable<IState>>(TestMethode));

            res.RunActions(states);

            Assert.Equal(DateTime.Parse("01.01.2022"), states.First().TimeStamp);
        }

        private void TestMethode(IEnumerable<IState> state)
        { 
            state.First().TimeStamp = DateTime.Parse("01.01.2022");
        }
    }
}
