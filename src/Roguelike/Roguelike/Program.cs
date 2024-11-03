using System;

public class Aid
{
    string name;
    uint recoverHP;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public uint RecoverHP {
        get { return recoverHP; }
        set {  recoverHP = value; }
    }
    public Aid(string name, uint recoverHP)
    {
        this.name = name;
        this.recoverHP = recoverHP;
    }
}
public class Weapon
{
    string name;
    uint durability;//сказано, что прочность должна быть, но не сказано, что оружие должно ломаться. Так что вот она, бесполезная
    uint damage;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public uint Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public Weapon(string name, uint damage)
    {
        this.name = name;
        this.durability = 1;
        this.damage = damage;
    }
}
public class Player
{
    string name;
    uint currentHP;
    uint maxHP;
    Aid aid;
    Weapon weapon;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public uint CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }
    public Aid Aid
    {
        get { return aid; }
        set { aid = value; }
    }
    public Weapon Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }
    public Player(string name, uint maxHP)
    {
        this.name = name;
        this.maxHP = maxHP;
        this.currentHP = maxHP;
    }
    public void useAid()
    {
        this.currentHP += this.aid.RecoverHP;
        if (this.currentHP > this.maxHP)
            this.currentHP = this.maxHP;
        this.aid = null;
    }
    public void Hit(Enemy enemy) {
        if(enemy.CurrentHP>=weapon.Damage)
            enemy.CurrentHP -= weapon.Damage;
        else
            enemy.CurrentHP = 0;
    }
}
public class Enemy
{
    string name;
    uint currentHP;
    uint maxHP;
    Weapon weapon;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public uint CurrentHP {
        get { return currentHP; }
        set { currentHP = value; }
    }
    public Enemy(uint maxHP)
    {
        this.maxHP = maxHP;
        this.currentHP = maxHP;
    }
    public Weapon Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }
    public void Hit(Player player)
    {
        if (player.CurrentHP >= weapon.Damage)
            player.CurrentHP -= weapon.Damage;
        else
            player.CurrentHP = 0;
    }
}
class Program
{
    static void Main(string[] args)
    {
        Random rnd = new Random();
        Weapon[] weapons = [new Weapon("Old Green Sword", 40), new Weapon("Knight sword", 20), new Weapon("King-Killer knife", 30), new Weapon("Dull axe", 5), new Weapon("Guard spear", 15)];
        Aid[] aids = [new Aid("small aid", 10), new Aid("usual aid", 20), new Aid("big aid", 40)];
        string[] enemyNames = ["Cultist", "Maniac", "Brigand", "Rogue", "Creature"];
        Console.WriteLine("Hello, warrior!\nCall yourself:");
        string playerName = Console.ReadLine();
        Player player = new Player(playerName, 100);
        int pointCount = 0;
        while (player.CurrentHP > 0)
        {
            Enemy enemy = new Enemy(Convert.ToUInt16(rnd.Next(100)));
            enemy.Name = enemyNames[rnd.Next(enemyNames.Length)];
            enemy.Weapon = weapons[rnd.Next(weapons.Length)];
            player.Aid = aids[rnd.Next(aids.Length)];
            player.Weapon = weapons[rnd.Next(weapons.Length)];
            Console.WriteLine($"\nHeaven sends {player.Name} {player.Weapon.Name} ({player.Weapon.Damage}) and {player.Aid.Name} ({player.Aid.RecoverHP}hp)");
            Console.WriteLine($"You have {player.CurrentHP} hp");
            Console.WriteLine($"\n{player.Name} meets enemy {enemy.Name} ({enemy.CurrentHP}hp). Enemy has {enemy.Weapon.Name} ({enemy.Weapon.Damage})");
            while (enemy.CurrentHP > 0 & player.CurrentHP > 0) {
                Console.WriteLine("\nWhat will you do?\n1: Hit enemy\n2: Use aid\nAny else: Miss a turn");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        {
                            player.Hit(enemy);
                            Console.WriteLine($"\n{player.Name} hits enemy {enemy.Name}");
                            break;
                        }
                    case "2":
                        {
                            if (player.Aid == null)
                                Console.WriteLine("\nYou used your aid already. You miss the turn");
                            else
                            {
                                player.useAid();
                                Console.WriteLine($"\n{player.Name} use aid");
                            }
                            break;

                        }
                }
                Console.WriteLine($"\nYou have {player.CurrentHP} hp");
                Console.WriteLine($"Your enemy has {enemy.CurrentHP} hp");
                if (enemy.CurrentHP > 0)
                {
                    enemy.Hit(player);
                    Console.WriteLine($"\n{enemy.Name} hits {player.Name}");
                    Console.WriteLine($"\nYou have {player.CurrentHP} hp");
                    Console.WriteLine($"Your enemy has {enemy.CurrentHP} hp");
                }
                else
                    pointCount++;
            }
        }
        Console.WriteLine($"\nCongratulations! You got {pointCount} points!");
    }
}