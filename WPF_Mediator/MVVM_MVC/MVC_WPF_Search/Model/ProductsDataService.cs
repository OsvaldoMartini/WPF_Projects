using System.Collections.Generic;
using System.Linq;

namespace MVC_WPF_Search.Model
{
    /// <summary>
    /// Data service that supplies Product data
    /// </summary>
    public class ProductsDataService
    {
        /// <summary>
        /// Gets the full list of Products
        /// </summary>
        /// <returns>Return the list of products</returns>
        public static IList<Product> GetAllProducts()
        {
            return sampleData.ToList();
        }

        /// <summary>
        /// Gets the list of Products matching the specified name
        /// </summary>
        /// <param name="name">The product name to search with</param>
        /// <returns>Return the list of products</returns>
        public static IList<Product> GetProductsByName(string name)
        {
            return (from prod in sampleData
                       where prod.Name.Contains(name)
                       select prod).ToList();
        }

        #region Sample Data 
        static List<Product> sampleData = new List<Product>
        {
            new Product 
            { 
                Name = "The Rear View Mirror Bluetooth Speakerphone", 
                Description = "This is the Bluetooth-enabled rear view mirror that displays incoming calls on the mirror itself and has a removable wireless headset that allows speakerphone communication or in-ear use. Calls are automatically routed to the in-ear headset when it is removed from the mirror, and return to the speakerphone when the headset is placed back into its cradle. The device connects wirelessly to any Bluetooth compatible cell phone, allowing hands-free conversations without the use of cumbersome wires and stand-alone speakers. The mirror clamps over your car's existing rearview mirror, the headset uses full-duplex noise- and echo-cancellation for clear audio, and the built-in loudspeaker is able to produce powerful sound up to 120db",
                Price = 120.55,
                Image = "../ProductImages/RearMirrorBlueTooth.jpg"
            },

            new Product 
            { 
                Name = "nuclear war USB hub", 
                Description = @"Had enough of being beaten into the submission by other USB Missile Launcher-wielding workmates? Just had a bad day with the boss? Then it's time to fall back on the ultimate sanction: the USB nuclear armageddon button. One push and... boom.
The unit - which conveniently doubles up as a four-port USB hub during peacetime - is equipped with a launch key, suitably military-looking toggle switches and, of course, the big red button, tucked under a protective flap to avoid an accidental apocalypse.
Should the balloon go up, however, and the button is pushed, the unit beeps out your four-minute warning to reach suitable shelter before... the end.",
                Price = 50.65,
                Image = "../ProductImages/usb_nuke_button_1.jpg"
            },

            new Product 
            { 
                Name = "Family Guy Stewie USB Toy", 
                Description = @"Plug and play with Stewie Interactive USB toy on your desktop. TV’s Family Guy’s Stewie mouths off and waves his ray gun in response to your keystrokes, movement and sounds. Ignore him and he’ll remind you who is boss with random phrases and actions. ",
                Price = 20.95,
                Image = "../ProductImages/StewieUSB.jpg"
            },

             new Product 
            { 
                Name = "The 40-Foot Marshmallow Blaster", 
                Description = @"This pump-action, pneumatic gun shoots sweet, edible marshmallows (or a handful of miniature marshmallows) up to 40'. The easy-to-refill bolt action design ensures fast, nonstop action.",
                Price = 45.95,
                Image = "../ProductImages/Marshmallow40Foot.jpg"
            },

             new Product 
            { 
                Name = "Simpsons USB Mouse/Mouse Pad Set", 
                Description = @"You can feel like a safety inspector at Springfield Nuclear Power Plant when you code with this Homer Simpson USB optical scroll mouse and mouse pad set. ",
                Price = 15.95,
                Image = "../ProductImages/HomerMouse.jpg"
            },

            new Product 
            { 
                Name = "Star Wars PC Speaker Set", 
                Description = @"For the die hard Star Wars Geek, these speakers will be a constant reminder that R2-D2 really does look like a kitchen trash can.",
                Price = 65.15,
                Image = "../ProductImages/r2_d2_speakers.jpg"
            },

            new Product 
            { 
                Name = "Digital Dog", 
                Description = @"To keep you company while you work. Start with the powerful bar magnet “body” and arrange the included bolts, nuts, twisted iron pieces and chain to create a one-of-a-kind dog. Rearrange the pieces to make your pet sit, lie down or even play dead. Components come in reusable tin box with black felt pouch",
                Price = 45.15,
                Image = "../ProductImages/DigitalDog.jpg"
            },

            new Product 
            { 
                Name = "Glow Brick", 
                Description = @"Yep - it's a glow-in-the-dark light bulb trapped inside a solid acrylic brick. The Glow Brick recharges from energy in natural sunlight during the day and glows at night. Not only that, it is actually made with a real light bulb! That's right.",
                Price = 35.95,
                Image = "../ProductImages/glow_brick_blue.jpg"
            },

            new Product 
            { 
                Name = "8-bit Tie", 
                Description = @"An 8-Bit tie - what an awesome way for the drones of Cubeland to show their independence from Corporate America! Silk-like Microfiber construction, clip on* (for easy dressing and t-shirt wear if needed). You'll be the envy of the office or, at least, you'll get tons of attention. You'll be just like Mario when he wore a tie to meetings when negotiating his contract with Nintendo. Sure the Wii, PS3, and Xbox 360 are out there with all their super technology, but sometimes it's nice to remember the beginnings of the video game revolution. Viva la 8-Bit! ",
                Price = 25.55,
                Image = "../ProductImages/8bit_tie2.jpg"
            },

            new Product 
            { 
                Name = "Mini LED Light", 
                Description = @"This Designer inspired mini LED DEK light will add a touch of panache to your desktop. This 4 ¾ ” tall DEK 2 that emulates the classic Arco lamp by Achille Castiglioni. ",
                Price = 15.35,
                Image = "../ProductImages/MiniLED.gif"
            },

            new Product 
            { 
                Name = "USB Missile Launcher", 
                Description = @"The USB Missile Launcher is the ultimate deterrent against those annoying people who lurk around your desk because they've nothing better to do. The Launcher holds three foam missiles, and Missile Command is located on your desktop (which is a great deal more convenient than having it buried under Cheyanne Mountain in Colorado - but that's Norad for you). You simply use your mouse to control the launcher which rotates and tilts as you zero in on your victim, that, despite being deeply childish, is immensely satisfying. The Missile Launcher fires its three foam missiles sequentially as you hit the 'Fire' button, and though collateral damage is minimal, the fun factor is exceedingly high. We just love USB toys",
                Price = 85.45,
                Image = "../ProductImages/USBMissileLauncher.jpg"
            },

            new Product 
            { 
                Name = "The Finger Drum Mousepad", 
                Description = @"This electronic drumpad also serves as a mousepad, and allows you to play eight different percussion sounds, including bass, snare, two rack toms, a floor tom, hi-hat, crash, and ride cymbals using only your fingers. A demonstration mode allows you to accompany six pre-set patterns, and you can record up to 30 of your own rhythms. Separate volume and tempo controls allow you to adjust sound levels",
                Price = 65.45,
                Image = "../ProductImages/DrumPad.jpg"
            },

            new Product 
            { 
                Name = "Desktop Repeater Rubber Band Gun", 
                Description = @"Use the Desktop Repeater to turn your office mail cart into a gatling-gun fortified war wagon. While yours might not have the firepower of the War Wagon John Wayne and Kirk Douglas encountered in the classic movie of the same name, this rapid-fire rubberband repeater will ensure your office mail gets delivered on schedule. Nothing clears a hallway or puts 'em back in their cubicles like sheer overwhelming firepower! ",
                Price = 15.25,
                Image = "../ProductImages/RubberBandMulti.jpg"
            },

            new Product 
            { 
                Name = "Water Clock", 
                Description = @"Just add water or any other electrolytic liquid (soda, coffee or even beer) into its fuel cell, set the time and date, and watch time go by on its digital display! The fuel cells extracts electrons from the electrolyte forming a steady stream of electrical current that drives the Water Clock. ",
                Price = 35.65,
                Image = "../ProductImages/WaterClock.jpg"
            },

             new Product 
            { 
                Name = "Soft Speakers Monkey", 
                Description = @"How about some speakers with a little cute 'n' cuddly personality? Switch out your computer's drab gray boxes for these Soft Speakers and, the next time you pop in a CD, you'll be doing the jungle boogie! Full-range stereo speakers come with USB or battery-powered inline amplifier and are compatible with iPods, CD and DVD players, PCs, Macs, and even standard cassette players! Plug and play with any device that features a standard audio jack. Each is approximately 7'' tall",
                Price = 95.15,
                Image = "../ProductImages/SpeakerMonkey.jpg"
            },

            new Product 
            { 
                Name = "Fighting GrandDads", 
                Description = @"Tired of powerful, chiseled athletes engaged in extreme fighting matches? These Racing Grandads are the perfect antidote, adding action, color and lots of laughs to your office. Though they may be getting up in age, they are full of competitive spirit, not willing to give an inch to their senior competition. Wind them up and let them go after each other with canes flailing and feet moving. And thank goodness, they claim to be free of the steroids and HGH substances that plague so many sports. Fight On, Grandad!! ",
                Price = 5.15,
                Image = "../ProductImages/FightingGrandDads.jpg"
            },

            new Product 
            { 
                Name = "Location Earth Dog Tags", 
                Description = @"Picture yourself lost in the galaxy...UFO sightings and Alien Abductions are on the rise...Will you return to tell the story?

In case of alien abduction these dog tags may save
your life. The crucial data an alien will need to get
you back to Earth is die stamped into these dog tags.",

                Price = 9.15,
                Image = "../ProductImages/dogtagimage.jpg"
            }

        };
        #endregion
    }
}
