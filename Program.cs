// See https://aka.ms/new-console-template for more information
using System;
namespace OOPFinal;

class Game
{
    static void Main()
    {
        Console.WriteLine("Welcome to my RPG Game!");

        static string Creation()
        {
            Console.WriteLine("What would you like to name your character?");
            string name = Console.ReadLine();
            return name;
        }

        string characterName = Creation();
        bool end = false;
        Player player = new Player();
        player.name = characterName;
        Weapon weapon = new Weapon();
        RandomNumbers rnd = new RandomNumbers();
        while (end == false)
        {
            Console.WriteLine("What would you like to do? fight, stats, equip or exit");
            string menuChoice = Console.ReadLine();
            int bonusDamage = weapon.CurrentWeaponDamage();
            int totalPlayerDamage = player.TotalAttackPower(bonusDamage);

            if (menuChoice == "stats")
            {
                Console.WriteLine("Your character's name is: " + player.name);
                Console.WriteLine("Your character's health is: " + player.health);
                Console.WriteLine("Your total attack power is: " + totalPlayerDamage);
                ;
            }
            else if (menuChoice == "exit")
            {
                end = true;
            }
            else if (menuChoice == "equip")
            {
                Console.WriteLine("You currently own a " + string.Join(", ", weapon.WeaponsOwned()));
                Console.WriteLine("What would you like to equip?");
                weapon.currentlyEquiped = Console.ReadLine();
                Console.WriteLine(weapon.currentlyEquiped + " has been equipped");
            }
            else if (menuChoice == "fight")
            {
                string monsterName = rnd.RandomMonster();
                Monster newMonster = new Monster();
                newMonster.name = monsterName;
                int monsterHealth = rnd.RandomHealth();
                int monsterDamage = rnd.RandomDamage();
                bool block = false;
                bool run = false;
                Console.WriteLine("You come across a " + monsterName);
                Console.WriteLine("It has " + monsterHealth + " health and does " + monsterDamage + " damage!");
                while (run == false && monsterHealth > 0 && player.health > 0)
                {
                    Console.WriteLine("What would you like to do? attack, block, heal, run");
                    string fightChoice = Console.ReadLine();
                    switch (fightChoice)
                    {
                        case "attack":
                            int swingDamage = totalPlayerDamage;
                            if (rnd.CheckForCrit())
                            {
                                swingDamage *= 2;
                                Console.WriteLine("You have managed to land a critical hit!");
                            }

                            Console.WriteLine("You swing your weapon at the " + monsterName + " and deal " +
                                              swingDamage + " damage!");
                            monsterHealth -= swingDamage;
                            if (monsterHealth <= 0)
                            {
                                Console.WriteLine("Congratulations the monster has been defeated!!!");
                                if (rnd.RandomWeapon() == "dagger")
                                {
                                    Console.WriteLine("You have found a dagger!");
                                    weapon.daggerOwned = true;
                                }
                                else if (rnd.RandomWeapon() == "sword")
                                {
                                    Console.WriteLine("You have found a sword!");
                                    weapon.swordOwned = true;
                                }
                                else if (rnd.RandomWeapon() == "axe")
                                {
                                    Console.WriteLine("You have found a axe!");
                                    weapon.axeOwned = true;
                                }
                                else if (rnd.RandomWeapon() == "hammer")
                                {
                                    Console.WriteLine("You have found a hammer!");
                                    weapon.hammerOwned = true;
                                }
                                else
                                {
                                    Console.WriteLine("Weapon");
                                }
                            }


                            break;
                        case "block":
                            Console.WriteLine("You have gotten ready to block the monsters next attack!");
                            block = true;
                            break;
                        case "heal":
                            Console.WriteLine("You have healed your character for 10 health");
                            player.Heal();
                            break;
                        case "run":
                            Console.WriteLine("You have attempted to run away!");
                            if (rnd.RunChance())
                            {
                                Console.WriteLine("You have successfully ran away!");
                                run = true;
                            }
                            else
                            {
                                Console.WriteLine("You have failed to run away!");
                                run = false;
                            }

                            break;
                        default:
                            Console.WriteLine("Please enter a valid option");
                            break;
                    }

                    if (monsterHealth > 0 && run == false)
                    {
                        int monsterTotalDamage = monsterDamage;
                        if (block)
                        {
                            monsterTotalDamage /= 3;
                        }

                        Console.WriteLine("The " + monsterName + " has swung his weapon at you and dealt " +
                                          monsterTotalDamage + " damage!");
                        player.health -= monsterTotalDamage;
                        if (newMonster.MonsterPower() == 1)
                        {
                            Console.WriteLine(
                                "The goblin uses his special power and throws a rock at you dealing 1 extra damage!");
                            player.health -= 1;
                        }
                        else if (newMonster.MonsterPower() == 2)
                        {
                            Console.WriteLine(
                                "The orc uses his special power and charges at you and slams into you dealing 2 extra damage!");
                            player.health -= 2;
                        }
                        else if (newMonster.MonsterPower() == 3)
                        {
                            Console.WriteLine("The troll uses his special power and ROARS healing for 3 health!");
                            monsterHealth += 3;
                        }
                        else
                        {
                            Console.WriteLine("The monster has no special power!");
                        }

                        Console.WriteLine("You have " + player.health + " health left");
                        Console.WriteLine("The " + monsterName + " now has " + monsterHealth + " health");
                        block = false;
                    }

                    if (player.health <= 0)
                    {
                        Console.WriteLine("You have been defeated!");
                        end = true;
                    }

                }
            }
            else
            {
                Console.WriteLine("Please enter a valid option");
            }
        }
    }
}

class RandomNumbers
{
    Random rnd = new Random();
    public bool CheckForCrit()
    {
        
        int crit = rnd.Next(1, 11);
        if (crit == 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string RandomMonster()
    {
        int monster = rnd.Next(1, 4);
        if (monster == 1)
        {
            return "goblin";
        }
        else if (monster == 2)
        {
            return "orc";
        }
        else if (monster == 3)
        {
            return "troll";
        }
        else
        {
            return "monster";
        }
    }
    public int RandomHealth()
    {
        int monsterHealth = rnd.Next(75, 176);
        return monsterHealth;
    }
    public int RandomDamage()
    {
        int monsterDamage = rnd.Next(3,12);
        return monsterDamage;
    }

    public bool RunChance()
    {
        int run = rnd.Next(1, 3);
        if (run == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string RandomWeapon()
    {
        int weaponNumber = rnd.Next(1, 5);
        switch (weaponNumber)
        {
            case 1:
                return "dagger";
            case 2:
                return "sword";
            case 3:
                return "axe";
            case 4:
                return "hammer";
            default:
                return "weapon";
        }
    }
}

abstract class Character
{
   
    public int health;
    public string name;
    public int baseAttackPower = 10;

    public abstract int TotalAttackPower(int bonus);
}

class Player : Character
{
    public int health = 100;
    public int Heal()
    {
        if (health <= 90)
        {
            return health += 10;
        }
        else if (health > 90 && health <= 100)
        {
            return health += (100 - health);
        }
        else
        {
            return health;
        } 
    }

    public override int TotalAttackPower(int bonus)
    {
        int totalPower = baseAttackPower + bonus;
        return totalPower;
    }
}

class Monster : Character
{
    public int MonsterPower()
    {
        if (name == "goblin")
        {
            return 1;
        }
        else if (name == "orc")
        {
            return 2;
        }
        else if (name == "troll")
        {
            return 3;
        }
        else
        {
            return 4;
        }
        
    }
    public override int TotalAttackPower(int bonus)
    {
        int totalPower = baseAttackPower + bonus;
        return totalPower;
    }
}

class Weapon
{
    public string daggerWeapon = "dagger";
    public int daggerDamage = 15;
    public bool daggerOwned = false;
    public string swordWeapon = "sword";
    public int swordDamage = 20;
    public bool swordOwned = false;
    public string axeWeapon = "axe";
    public int axeDamage = 20;
    public bool axeOwned = false;
    public string hammerWeapon = "hammer";
    public int hammerDamage = 25;
    public bool hammerOwned = false;
    public string currentlyEquiped;

    public int CurrentWeaponDamage()
    {
        if (currentlyEquiped == "dagger" && daggerOwned == true)
        {
            return daggerDamage;
        }
        else if (currentlyEquiped == "sword" && swordOwned == true)
        {
            return swordDamage;
        }
        else if (currentlyEquiped == "axe" && axeOwned == true)
        {
            return axeDamage;
        }
        else if (currentlyEquiped == "hammer" && hammerOwned == true)
        {
            return hammerDamage;
        }
        else
        {
            return 0;
        }
    }
    public List<string> WeaponsOwned()
    {
        List<string> weapons = new List<string>();
        if (daggerOwned == true)
        {
            weapons.Add(daggerWeapon);
        }

        if (swordOwned == true)
        {
            weapons.Add(swordWeapon);
        }

        if (axeOwned == true)
        {
            weapons.Add(axeWeapon);
        }

        if (hammerOwned == true)
        {
            weapons.Add(hammerWeapon);
        }
        return weapons;
    }
}