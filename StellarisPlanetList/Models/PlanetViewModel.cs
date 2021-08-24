using System;
using System.ComponentModel.DataAnnotations;

namespace StellarisPlanetList.Models
{
    public class PlanetViewModel
    {
        [Key]
        public int PlanetId { get; set; }

        /// <summary>
        /// Nmae of the planet in the game
        /// Which is either a special name like Axel
        /// Or a name like Sol 3 3rd planet from the star sol
        /// </summary>
        [Required]
        [StringLength(180)]
        public string PlanetName { get; set; }

        /// <summary>
        /// Thye wiki does not list all planets avliabale do not use it
        /// This tells you what type of planet it is
        /// It can be Gaia, Tomb, Tropical, Continental, Ocean, Arctic, Tundra,
        /// Alpine, Desert, Arid, Savanna, Relic, archaeological, Terrafromable,
        /// and Driod/Robot theres other planet types but you can settle pops on them
        /// pops being population meaning thier essentialy useless
        /// We also have ring worlds, habbittates and other ones which will be includined
        /// meaning it will be open ended and no drop downs (mods also have special planets)
        /// </summary>
        [Required]
        [StringLength(180)]
        public string PlanetType { get; set; }

        /// <summary>
        /// this just refers to the planet size for pop to settle on
        /// i belive its anywhere from 4 but ill set it as one since its hard to get a number
        /// to 25 standard size but ive seen them get really high so it well have no max size
        /// Ive seen people cliam 28, 30, and some other ones can get up to 120
        /// again no drop down but min number will be set to be one or greater and it cant go over 360
        /// </summary>
        [Required]
        public int PlanetSize { get; set; }

        /// <summary>
        /// What is the % your species will like the planet from 0% to 100% it can go over 
        /// 100 but you dont get bonuses from that so it be a 1 to 100 type in box
        /// </summary>
        public int PlanetPercenage { get; set; }

        /// <summary>
        /// These are effect the planet can get such as high gravity, low gravitiy
        /// mega fuanna, Aleins, a beep saying its getting dark and my battery is getting low,
        /// and Holy world, as its a moddifer not a planet this will be separate from notes.
        /// </summary>
        [StringLength(300)]
        public string PlanetEffects { get; set; }

        /// <summary>
        /// the number that links this planet to a user, as the user creates
        /// a planet the planet becomes linked to the user via a number.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// implermented now for issue number 4, note section for each planet but 
        /// i will have to incorptrate it unqiuely into the planet view section
        /// Most likley will open a pop up to display information or a scroll box of some sort undeicded as of now
        /// </summary>
        [StringLength(4000)]
        public string PlanetNotes { get; set; }
    }
}
