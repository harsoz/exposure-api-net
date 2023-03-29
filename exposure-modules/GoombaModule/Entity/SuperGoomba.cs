using exposure_modules.GoombaModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.GoombaModule.Entity
{
    public class SuperGoomba : IGoomba
    {
        /// <summary>
        /// size of goomba, default is 10
        /// </summary>
        public int size { get; set; } = 40;

        /// <summary>
        /// attack of goomba, default is 10
        /// </summary>
        public int attack { get; set; } = 45;

        /// <summary>
        /// defense of goomba, default is 10
        /// </summary>
        public int defense { get; set; } = 50;

        public SuperGoomba()
        {
        }

        public SuperGoomba(int attack)
        {
            this.attack = attack;
        }

        public SuperGoomba(int attack, int defense)
        {
            this.attack = attack;
            this.defense = defense;
        }
    }
}
