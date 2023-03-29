using exposure_modules.GoombaModule.Entity;
using exposure_modules.GoombaModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace exposure_modules.GoombaModule.Logic
{
    /// <summary>
    /// Factory for goombas
    /// </summary>
    public class GoombaLogic: IGoombaLogic
    {
        /// <summary>
        /// Generic factory for goombas
        /// </summary>
        /// <returns></returns>
        public IGoomba GenerateGoomba()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] randomNumber = new byte[4]; // 4 bytes = 32 bits
            rng.GetBytes(randomNumber);
            int randomInt = BitConverter.ToInt32(randomNumber, 0);
            int level = 100;

            if (randomInt < level) {
                return new SuperGoomba();
            }
            return new MiniGoomba();
        }

        /// <summary>
        /// Speficic factory for goomba
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IGoomba GenerateSpecificGoomba(string className)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes()
                .First(t => t.Name == className);

            return (IGoomba) Activator.CreateInstance(type);
        }

        /// <summary>
        /// Custom factory for goombas
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="attack"></param>
        /// <param name="defense"></param>
        /// <returns>Goomba</returns>
        public IGoomba GenerateCustomGoomba(string className, int size, int attack, int defense)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var type = assembly.GetTypes()
                .First(t => t.Name == className);

            var goomba = (IGoomba) Activator.CreateInstance(type);

            goomba.size = size > goomba.size ? goomba.size : size;
            goomba.attack = attack > goomba.attack ? goomba.attack : attack;
            goomba.defense = defense > goomba.defense ? goomba.defense : defense;

            return goomba;
        }
    }
}
