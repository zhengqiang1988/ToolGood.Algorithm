﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PetaTest;

namespace ToolGood.Algorithm
{
    [TestFixture]
    class AlgorithmEngineTest
    {
        [Test]
        public void Test()
        {
            AlgorithmEngine engine = new AlgorithmEngine();

            double t = 0.0;
            if (engine.Parse("1+2")) {
                t = (double)engine.Evaluate().NumberValue;
            }
            Assert.AreEqual(3.0, t);

            var c = engine.TryEvaluate("2+3", 0);
            Assert.AreEqual(5, c);
            c = engine.TryEvaluate("(2)+3", 0);
            Assert.AreEqual(5, c);
            c = engine.TryEvaluate("2+3*2+10/2*4", 0);
            Assert.AreEqual(28, c);

            c = engine.TryEvaluate("if(2+3*2+10/2*4,1", 0);
            Assert.AreEqual(0, c);



            var e = engine.TryEvaluate("e", 0.0);
            Assert.AreEqual(Math.E, e);
            e = engine.TryEvaluate("pi", 0.0);
            Assert.AreEqual(Math.PI, e);

            var b = engine.TryEvaluate("true", true);
            Assert.AreEqual(true, b);
            b = engine.TryEvaluate("false", false);
            Assert.AreEqual(false, b);

            var b1 = engine.TryEvaluate("if(true,1,2)", 0);
            Assert.AreEqual(1, b1);

            b1 = engine.TryEvaluate("if(false,1,2)", 0);
            Assert.AreEqual(2, b1);

            var b2 = engine.TryEvaluate("pi*4", 0.0);
            Assert.AreEqual(Math.PI * 4, b2);
            b2 = engine.TryEvaluate("e*4", 0.0);
            Assert.AreEqual(Math.E * 4, b2);

            var s = engine.TryEvaluate("'aa'&'bb'", "");
            Assert.AreEqual("aabb", s);

            var r = engine.TryEvaluate("count({ 1,2,3,4})", 0);
            Assert.AreEqual(4, r);


            r = engine.TryEvaluate("(1=1)*9+2", 0);
            Assert.AreEqual(11, r); ;
            r = engine.TryEvaluate("(1=2)*9+2", 0);
            Assert.AreEqual(2, r); ;

            var dt = engine.TryEvaluate("'2016-1-1'+1", DateTime.MinValue);
            Assert.AreEqual(DateTime.Parse("2016-1-2"), dt); ;
            dt = engine.TryEvaluate("'2016-1-1'+9*'1:0'", DateTime.MinValue);
            Assert.AreEqual(DateTime.Parse("2016-1-1 9:0"), dt); ;

            var value = engine.TryEvaluate("1 > (-2)", false);
            Assert.AreEqual(value, true);


            value = engine.TryEvaluate("(-1) > (-2）", false);
            Assert.AreEqual(value, true);


            value = engine.TryEvaluate("-1 > (-2)", false);
            Assert.AreEqual(value, true);

            value = engine.TryEvaluate("-1 > -2", false);
            Assert.AreEqual(value, true);

            var value2 = engine.TryEvaluate("-1 > -2", false);
            Assert.AreEqual(value2, true);


            var value3 = engine.TryEvaluate("-7 < -2", false);
            Assert.AreEqual(value3, true);
        }

        [Test]
        public void base_test()
        {
            AlgorithmEngine engine = new AlgorithmEngine();
            var t = engine.TryEvaluate("1+(3*2+2)/2", 0);
            Assert.AreEqual(5, t);

            t = engine.TryEvaluate("(8-3)*(3+2)", 0);
            Assert.AreEqual(25, t);

            t = engine.TryEvaluate("(8-3)*(3+2) % 7", 0);
            Assert.AreEqual(4, t);

            var b = engine.TryEvaluate("1=1", false);
            Assert.AreEqual(true, b);

            b = engine.TryEvaluate("1=2", false);
            Assert.AreEqual(false, b);

            b = engine.TryEvaluate("1<>2", false);
            Assert.AreEqual(true, b);

            b = engine.TryEvaluate("1!=2", false);
            Assert.AreEqual(true, b);

            b = engine.TryEvaluate("1>2", false);
            Assert.AreEqual(false, b);

            b = engine.TryEvaluate("1<2", false);
            Assert.AreEqual(true, b);

            b = engine.TryEvaluate("1<=2", false);
            Assert.AreEqual(true, b);

            b = engine.TryEvaluate("1>=2", false);
            Assert.AreEqual(false, b);

        }

        [Test]
        public void base_test2()
        {
            AlgorithmEngine engine = new AlgorithmEngine();
            var t = engine.TryEvaluate("1+(3*2+2)/2 & '11' & '11:20'*9 & isnumber(22)*3", "");

        }

        [Test]
        public void Cylinder_Test()
        {
            Cylinder c = new Cylinder(3, 10);
            var t = c.TryEvaluate("[半径]*[半径]*pi()", 0.0);      //圆底面积
            t = c.TryEvaluate("[直径]*pi()", 0.0);            //圆的长
            t = c.TryEvaluate("[半径]*[半径]*pi()*[高]", 0.0); //圆的体积

            if (c.Parse("[直径1]*pi()") == false) {
                Assert.AreEqual("参数[直径1]无效!", c.LastError);
            }

            t = c.TryEvaluate("['半径']*[半径]*pi()*[高]", 0.0); //圆的体积

            t = c.TryEvaluate("求面积（10）", 0.0); //圆的体积
            Assert.AreEqual(10 * 10 * Math.PI, t);



            var json = "{'灰色':'L','canBookCount':905,'saleCount':91,'specId':'43b0e72e98731aed69e1f0cc7d64bf4d'}";
            c.AddParameterFromJson(json);


            var tt = c.TryEvaluate("['灰色']", ""); //圆的体积
            Assert.AreEqual("L", tt);

        }

    }
}
