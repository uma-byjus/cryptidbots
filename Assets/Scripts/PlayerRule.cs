using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CBot
{

    public static class Extensions
    {

        public static void FormRule(this Rule r, string s)
        {
            string ls = s.ToLower();
            if (ls.Contains("not"))
            {
                r.not = true;
            }
            if(ls.Contains("one") && ls.Contains("space"))
            {
                r.rSpaces = 1;
            }
            if (ls.Contains("spaces"))
            {
                if (ls.Contains("two"))
                {
                    r.rSpaces = 2;
                }
                if (ls.Contains("three"))
                {
                    r.rSpaces = 3;
                }
            }
            string[] ts = (string[])System.Enum.GetNames(typeof(TerrainType));
            for (int i = 0; i < ts.Length; i++)
            {
                if (ls.Contains(ts[i].ToLower()))
                {
                    r.rTerrainTypes.Add(i);
                }
            }

            string[] ans = (string[])System.Enum.GetNames(typeof(Region));
            if (ls.Contains("animal"))
            {
                r.rRegions.Add(1);
                r.rRegions.Add(2);
            }
            else
            {
                for (int i = 1; i < ans.Length; i++)
                {
                    if (ls.Contains(ans[i].ToLower()))
                    {
                        r.rRegions.Add(i);
                    }
                }
            }

            if (ls.Contains("stone"))
            {
                r.rStructures.Add(0);
                r.rStructures.Add(2);
                r.rStructures.Add(4);
                r.rStructures.Add(6);
            }

            if (ls.Contains("shack"))
            {
                r.rStructures.Add(1);
                r.rStructures.Add(3);
                r.rStructures.Add(5);
                r.rStructures.Add(7);
            }

            if (ls.Contains("structure"))
            {
                if (ls.Contains("blue"))
                {
                    r.rStructures.Add(0);
                    r.rStructures.Add(1);
                }
                if (ls.Contains("green"))
                {
                    r.rStructures.Add(2);
                    r.rStructures.Add(3);
                }
                if (ls.Contains("white"))
                {
                    r.rStructures.Add(4);
                    r.rStructures.Add(5);
                }
                if (ls.Contains("black"))
                {
                    r.rStructures.Add(6);
                    r.rStructures.Add(7);
                }
            }


        }
    }

    [CreateAssetMenu(fileName = "Player Rule", menuName = "Create / Player Rule Data", order = 1)]
    public class PlayerRule : ScriptableObject
    {
        public string singleRuleInWord;
        public Rule CurrentRule;
        public List<string> RulesInWord = new List<string>();
        public List<Rule> rule = new List<Rule>();

        public void SetRules()
        {
            rule = new List<Rule>();
            foreach (string r in RulesInWord)
            {
                Rule ru = new Rule();
                ru.FormRule(r);
                rule.Add(ru);
            }

            CurrentRule.FormRule(singleRuleInWord);
        }

    }
}
