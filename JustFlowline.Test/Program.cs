using JustFlowline.Extensions;
using JustFlowline.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JustFlowline.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddFlowline();
            var serviceProvider = services.Build();
            var host = serviceProvider.GetService<IFlowlineHost>();
            var mathParam = new DicEx();
            mathParam["AddNum1"] = 10;
            mathParam["AddNum2"] = 22;

            var mathParam2 = new Data
            {
                AddParam = new AddParam() { Num1 = 10, Num2 = 22 },
                MultiParam = new MultiParam(),
            };

            var mathParam3 = new Dictionary<string, object>();
            mathParam3["AddNum1"] = 10;
            mathParam3["AddNum2"] = 22;

            Expression<Func<int, int>> expression = p => mathParam["AddNum1"];
            expression.Compile().Invoke(13);
            //host.RegisterFlowline<MathFlowline, DicEx>();
            host.RegisterFlowline<MathFlowline2, Data>();
            host.RegisterFlowline<MathFlowline3, Dictionary<string,object>>();
            host.Start();
            //var flowlineId = host.StartFlowline("MathFlowline", mathParam).Result;
            //var flowlineId2 = host.StartFlowline("MathFlowline2", mathParam2).Result;
            var flowlineId3 = host.StartFlowline("MathFlowline3", mathParam3).Result;

            Console.ReadLine();
        }
    }
}
