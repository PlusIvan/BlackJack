using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BlackJack.template
{
    class ASCII
    {
        private readonly JObject alpha = new JObject
        {
            { "c",  @" 
 __ 
/   
\__ 
" },
            { "a",  @" 
 /\  
/--\ 
" },
            { "s",  @" 
 __ 
(_  
__)
" },
            { "h",  @" 
|__| 
|  | 
" },
        };
        private readonly string[] numbers = {
@"
  __
 /  \ 
 \__/ 
",@"  
 /| 
  | 
",@"  
 __  
  _) 
 /__ 
",@"  
 __  
  _) 
 __) 
",@"     
 |__| 
    | 
",@"  
  __ 
 |_  
 __) 
",@"  
  __  
 /__  
 \__) 
",@"  
 ___ 
   / 
  /  
",@"  
  __  
 (__) 
 (__)  
",@"  
  __  
 (__\ 
  __/  
"
};
        public string Number(int a)
        {
            string new_a = a.ToString();
            string render = "";
            for (var x = 0; x < new_a.Length; x++)
            {
                render += numbers[new_a[x]];
            }
            return render;
        }
        public String Text(String a)
        {
            string render = "";
                foreach (JProperty property in alpha.Properties())
                {
                    // Console.WriteLine($"[{property.Name}] {property.Value}");
                    if (property.Name == a)
                    {
                    render += @property.Value;
                    }
                }
            return render;
        }

    }
}
