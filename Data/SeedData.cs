using System;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AppDbContext>>());

        if (context?.Studios is null)
        {
            throw new NullReferenceException(
                "Null AppDbContext or Movie DbSet");
        }

        if (!context.Studios.Any())
        {
            context.Studios.Add(
                new Studio
                {
                    Name = "Warner Bros. Entertainment",
                    Movies = new List<Movie>
                    {
                        new Movie
                        {
                            Title = "Mad Max",
                            ReleaseDate = new DateOnly(1979, 4, 12),
                            Genre = "Sci-fi (Cyberpunk)",
                            Price = 2.51M,
                        },
                        new Movie
                        {
                            Title = "The Road Warrior",
                            ReleaseDate = new DateOnly(1981, 12, 24),
                            Genre = "Sci-fi (Cyberpunk)",
                            Price = 2.78M,
                        },
                        new Movie
                        {
                            Title = "Mad Max: Beyond Thunderdome",
                            ReleaseDate = new DateOnly(1985, 7, 10),
                            Genre = "Sci-fi (Cyberpunk)",
                            Price = 3.55M,
                        },
                        new Movie
                        {
                            Title = "Mad Max: Fury Road",
                            ReleaseDate = new DateOnly(2015, 5, 15),
                            Genre = "Sci-fi (Cyberpunk)",
                            Price = 8.43M,
                        },
                        new Movie
                        {
                            Title = "Furiosa: A Mad Max Saga",
                            ReleaseDate = new DateOnly(2024, 5, 24),
                            Genre = "Sci-fi (Cyberpunk)",
                            Price = 13.49M,
                        }
                    }
                }
            );
        }

        if (!context.Actors.Any())
        {
            context.Actors.AddRange(
                new List<Actor>
                {
                    new Actor{Name="Mel Gibson"},
                    new Actor{Name="Hugh Keys-Byrne"},
                    new Actor{Name="Joanne Samuel"},
                    new Actor{Name="Steve Bisley"},
                    new Actor{Name="Tim Burns"},

                    new Actor{Name="Bruce Spence"},
                    new Actor{Name="Michael Preston"},
                    new Actor{Name="Max Phipps"},
                    new Actor{Name="Vernon Wells"},
                    new Actor{Name="Kyell Nilsson"},

                    new Actor{Name="Tina Turner"},
                    new Actor{Name="Adam Cockburn"},
                    new Actor{Name="Frank Thring"},
                    new Actor{Name="Angelo Rossito"},
                    new Actor{Name="Paul Larsson"},

                    new Actor{Name="Tom Hardy"},
                    new Actor{Name="Charlize Theron"},
                    new Actor{Name="Nicholas Hoult"},
                    new Actor{Name="Josh Helman"},
                    new Actor{Name="Nathan Jones"},

                    new Actor{Name="Anya-Taylor Joy"},
                    new Actor{Name="Chris Hemsworth"},
                    new Actor{Name="Tom Burke"},
                    new Actor{Name="Alyla Browne"},
                    new Actor{Name="George Shevtsov"},
                    new Actor{Name="Lachy Hulme"}
                }
            );
        }
        context.SaveChanges();
    }
}
