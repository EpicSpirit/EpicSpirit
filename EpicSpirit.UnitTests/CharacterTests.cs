using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using EpicSpirit.Game;
using UnityEngine;

namespace EpicSpirit.UnitTests
{
    [TestFixture]
    public class CharacterTests
    {
        [Test]
        public void Is_PowerTwo_return_the_correct_value_()
        {
            Character testCharacter = new GenericCharacterForUnitTests();
            Assert.That( 2 == testCharacter.PowerTwo( 4 ) );
        }
    }
}
