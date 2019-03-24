using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    // Start is called before the first frame update
   public struct GameCharacter
    {
        public string name;
        public RockPaperSissor.WEAPONTYPE weapon;
        public RockPaperSissor.CHARACTERTYPE character;
        public bool used;
    }

    private Dictionary<string, GameCharacter> heros;

    public GameData()
    {
        resetHeroList();
    }

    public void resetHeroList()
    {
        heros = new Dictionary<string, GameCharacter>()
            {
                { "Nandhimithra", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Nandhimithra",weapon = RockPaperSissor.WEAPONTYPE.shield, used = false  }  },
                { "Suranimala", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Suranimala",weapon = RockPaperSissor.WEAPONTYPE.sword, used = false  }  },
                { "Gotaimbara", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Gotaimbara",weapon = RockPaperSissor.WEAPONTYPE.shield, used = false  }  },
                { "Theraputthabhya", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Theraputthabhya",weapon = RockPaperSissor.WEAPONTYPE.sphere, used = false  }  },
                { "Mahabharana", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Mahabharana",weapon = RockPaperSissor.WEAPONTYPE.shield, used = false  }  },
                { "Velusumanna", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Velusumanna",weapon = RockPaperSissor.WEAPONTYPE.sphere, used = false  }  },
                { "Khanjadeva", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Khanjadeva",weapon = RockPaperSissor.WEAPONTYPE.sword, used = false  }  },
                { "Phussadeva", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Phussadeva",weapon = RockPaperSissor.WEAPONTYPE.sword, used = false  }  },
                { "Labhiyavasabha", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Labhiyavasabha",weapon = RockPaperSissor.WEAPONTYPE.sword, used = false  }  },
                { "Mahasona", new GameCharacter{ character = RockPaperSissor.CHARACTERTYPE.YODAYA,name ="Mahasona",weapon = RockPaperSissor.WEAPONTYPE.sphere, used = false  }  }
            };
    }

    public RockPaperSissor.WEAPONTYPE getSelectedCharacterWeaponType( string characterName)
    {
        return heros[characterName].weapon;
    }

    public GameCharacter getCharacter(string name)
    {
        return heros[name];
    }

    public Dictionary<string, GameCharacter> getAllHeros()
    {
        return heros;
    }

    public void setCharacterUsed(string name)
    {
        GameCharacter character = heros[name];
        character.used = true;
        heros[name] = character;
    }
}
