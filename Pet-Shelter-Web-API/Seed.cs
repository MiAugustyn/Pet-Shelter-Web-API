using PetShelterWebAPI.Data;
using PetShelterWebAPI.Models;
using PetShelterWebAPI.Models.JoinTables;

namespace PetShelterWebAPI
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            var Shelters = _context.Shelters.ToList();

            if (!_context.Shelters.Any())
            {
                Shelters = new List<Shelter>()
                {
                    new Shelter()
                    {
                        Address =  "ul. Weissa 86, Kraków, 31-313, Poland",
                        Workers = new List<Worker>()
                        {
                            new Worker()
                            {
                                Name = "John",
                                Surname = "Doe",
                                PhoneNumber = 123456789,
                                Email = "john.doe@example.com",
                                DateOfBirth = new DateOnly(1990, 1, 1),
                            },

                            new Worker()
                            {
                                Name = "Jane",
                                Surname = "Smith",
                                PhoneNumber = 987654321,
                                Email = "jane.smith@example.com",
                                DateOfBirth = new DateOnly(1985, 5, 15),
                            },

                            new Worker()
                            {
                                Name = "Alice",
                                Surname = "Johnson",
                                PhoneNumber = 555123456,
                                Email = "alice.johnson@example.com",
                                DateOfBirth = new DateOnly(1995, 10, 30),

                            }
                        }
                    },

                    new Shelter()
                    {
                        Address = "ul. Armii Krajowej 18, Bydgoszcz, 31-313, Poland",
                        Workers = new List<Worker>()
                        {
                            new Worker()
                            {
                                Name = "Michael",
                                Surname = "Brown",
                                PhoneNumber = 444987654,
                                Email = "michael.brown@example.com",
                                DateOfBirth = new DateOnly(1988, 3, 22)
                            },

                            new Worker()
                            {
                                Name = "Emily",
                                Surname = "Davis",
                                PhoneNumber = 333876543,
                                Email = "emily.davis@example.com",
                                DateOfBirth = new DateOnly(1992, 7, 14)
                            },

                            new Worker()
                            {
                                Name = "David",
                                Surname = "Wilson",
                                PhoneNumber = 222765432,
                                Email = "david.wilson@example.com",
                                DateOfBirth = new DateOnly(1983, 11, 5)
                            }
                        }
                    },
                };

                _context.Shelters.AddRange(Shelters);
                _context.SaveChanges();
            }

            if (!_context.PetOwners.Any())
            {
                var Workers = new List<Worker>();
                try
                {
                    Workers = Shelters.SelectMany(s => s.Workers).ToList();
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    var PetOwners = new List<PetOwner>()
                    {
                        new PetOwner()
                        {
                            Pet = new Pet()
                            {
                                Name = "Sasanka",
                                Age = 1,
                                Specie = new Specie() { Name = "Gerbil" },
                                Breed = new Breed() { Name = "Mongolian Gerbil" },
                                Shelter = Shelters.FirstOrDefault(s => s.Id == 1),
                                Notes = new List<Note>()
                                {
                                    new Note()
                                    {
                                        Description = "Sasanka loves running on a wheel and digging in a sand bath",
                                        Worker = Workers[0]
                                    },

                                    new Note()
                                    {
                                        Description = "Regularly check for signs of illness (lethargy or ruffled fur).",
                                        Worker = Workers[1]
                                    }
                                }
                            },


                            Owner = new Owner()
                            {
                                Name = "Mary",
                                Surname = "Sue",
                                PhoneNumber = 111222333,
                                Email = "mary.sue@example.com",

                            }
                        },

                        new PetOwner()
                        {
                            Pet = new Pet()
                            {
                                Name = "Buddy",
                                Age = 3,
                                Specie = new Specie() { Name = "Dog" },
                                Breed = new Breed() { Name = "Golden Retriever" },
                                Shelter = Shelters[1],
                                Notes = new List<Note>()
                                {
                                    new Note()
                                    {
                                        Description = "Buddy loves playing fetch and swimming.",
                                        Worker = Workers[2]
                                    },
                                    new Note()
                                    {
                                        Description = "Ensure regular grooming to avoid matting.",
                                        Worker = Workers[3],
                                    }
                                }
                            },

                            Owner = new Owner()
                            {
                                Name = "Michael",
                                Surname = "Augusto",
                                PhoneNumber = 222333444,
                                Email = "michael.augusto@example.com",
                            }
                        },

                        new PetOwner()
                        {
                            Pet = new Pet()
                            {
                                Name = "Whiskers",
                                Age = 2,
                                Specie = new Specie() { Name = "Cat" },
                                Breed = new Breed() { Name = "Siamese" },
                                Shelter = Shelters[0],
                                Notes = new List<Note>()
                                {
                                    new Note()
                                    {
                                        Description = "Whiskers enjoys climbing and playing with toys.",
                                        Worker = Workers[4]
                                    },
                                    new Note()
                                    {
                                        Description = "Monitor for any signs of dental issues.",
                                        Worker = Workers[5]
                                    }
                                }
                            },

                            Owner = new Owner()
                            {
                                Name = "Jane",
                                Surname = "Smith",
                                PhoneNumber = 333444555,
                                Email = "jane.smith@example.com",
                            }
                        },

                        new PetOwner()
                        {
                            Pet = new Pet()
                            {
                                Name = "Nibbles",
                                Age = 1,
                                Specie = new Specie() { Name = "Rabbit" },
                                Breed = new Breed() { Name = "Netherland Dwarf" },
                                Shelter = Shelters[1],
                                Notes = new List<Note>()
                                {
                                    new Note()
                                    {
                                        Description = "Nibbles loves chewing on hay and exploring.",
                                        Worker = Workers[0]
                                    },
                                    new Note()
                                    {
                                        Description = "Check for overgrown teeth regularly.",
                                        Worker = Workers[1]
                                    }
                                }
                            },

                            Owner = new Owner()
                            {
                                Name = "Alice",
                                Surname = "Johnson",
                                PhoneNumber = 444555666,
                                Email = "alice.johnson@example.com",
                            }
                        },

                        new PetOwner()
                        {
                            Pet = new Pet()
                            {
                                Name = "Spike",
                                Age = 4,
                                Specie = new Specie() { Name = "Hedgehog" },
                                Breed = new Breed() { Name = "African Pygmy" },
                                Shelter = Shelters[0],
                                Notes = new List<Note>()
                                {
                                    new Note()
                                    {
                                        Description = "Spike enjoys burrowing and eating insects.",
                                        Worker = Workers[2]
                                    },
                                    new Note()
                                    {
                                        Description = "Ensure a warm environment to prevent hibernation.",
                                        Worker = Workers[3]
                                    }
                                }
                            },

                            Owner = new Owner()
                            {
                                Name = "Bob",
                                Surname = "Brown",
                                PhoneNumber = 555666777,
                                Email = "bob.brown@example.com",
                            }
                        },
                    };

                    _context.PetOwners.AddRange(PetOwners);
                    _context.SaveChanges();
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("An error occured while seeding database");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}