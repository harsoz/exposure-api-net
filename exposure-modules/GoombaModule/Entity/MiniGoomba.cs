using exposure_modules.GoombaModule.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exposure_modules.GoombaModule.Entity
{
    public class MiniGoomba : IGoomba
    {
        /// <summary>
        /// size of goomba, default is 10
        /// </summary>
        public int size { get; set; } = 10;

        /// <summary>
        /// attack of goomba, default is 10
        /// </summary>
        public int attack { get; set; } = 10;

        /// <summary>
        /// defense of goomba, default is 10
        /// </summary>
        public int defense { get; set; } = 10;

        public MiniGoomba()
        {
        }

        public MiniGoomba(int attack)
        {
            this.attack = attack;
        }

        public MiniGoomba(int attack, int defense)
        {
            this.attack = attack;
            this.defense = defense;
        }

    }
}
