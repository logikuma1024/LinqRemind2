using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqRemind2
{
    /// <summary>
    /// テスト用データクラス
    /// </summary>
    class Person
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }
    }

    class Program
    {
        /// <summary>
        /// テストデータ
        /// </summary>
        private static List<Person> members = new List<Person>
        {
            new Person {Name = "Aさん", Sex = "男", Age = 20, Score = 80 },
            new Person {Name = "Bさん", Sex = "女", Age = 34, Score = 70 },
            new Person {Name = "Cさん", Sex = "男", Age = 58, Score = 61 },
            new Person {Name = "Dさん", Sex = "男", Age = 29, Score = 50 },
            new Person {Name = "Eさん", Sex = "女", Age = 18, Score = 98 }
        };

        /// <summary>
        /// Application Entry
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //集計演算子編
            //SUM
            //SumTest();

            //COUNT
            //CountTest();

            //AVARAGE
            //AvarageTest();

            //MAX
            //MaxTest();

            //MIN
            //MinTest();

            //AGGREGATE
            //AggregateTest();
            AggregateTest2();

            Console.ReadLine();
        }

        #region 集約関数編

        /// <summary>
        /// Sum関数のテスト
        /// </summary>
        static void SumTest()
        {
            //<Scoreの合計を抽出します>
            //foreach版-----------------------------------------------

            //結果を格納する変数を作成する。
            int ret1 = 0;

            foreach (var m in members)
                //値を足していく。
                ret1 += m.Score;

            //Linq版--------------------------------------------------

            //メンバー表の中からスコアの合計を求めます。
            var ret2 = members.Sum(x => x.Score);

            //表示する------------------------------------------------
            Console.WriteLine($"foreach:{ret1}, Linq:{ret2}");
        }

        /// <summary>
        /// Count関数のテスト
        /// </summary>
        static void CountTest()
        {
            //<男性の数をカウントします>
            //foreach版-----------------------------------------------

            //結果を格納する変数を作成する。
            int ret1 = 0;

            foreach(var mem in members)
            {
                if (mem.Sex == "男")
                    //男性の場合はカウントアップ
                    ret1++;
            }

            //Linq版--------------------------------------------------
            
            //membersの中で性別が男性の個数を返します。
            var ret2 = members.Count(x => x.Sex == "男");

            //表示する------------------------------------------------
            Console.WriteLine($"foreach:{ret1}, Linq:{ret2}");
        }

        /// <summary>
        /// Avarage関数のテスト
        /// </summary>
        static void AvarageTest()
        {
            //<Ageの平均値を集計します。>
            //foreach版-----------------------------------------------

            //計算用の合計値
            double sum = 0;
            //計算用の件数
            int count = 0;
            
            foreach(var mem in members)
            {
                //合計値と件数をカウントアップ
                sum += mem.Age;
                count++;
            }

            //平均値を算出する
            double ret1 = sum / count;

            //Linq版--------------------------------------------------

            //membersの年齢の平均値を算出する
            var ret2 = members.Average(x => x.Age);

            //表示する------------------------------------------------
            Console.WriteLine($"foreach:{ret1}, Linq:{ret2}");
        }

        /// <summary>
        /// Max関数のテスト
        /// </summary>
        static void MaxTest()
        {
            //<Scoreの最大値を集計します。>
            //foreach版-----------------------------------------------

            //最大値を格納する変数
            int ret1 = 0;

            foreach(var mem in members)
            {
                if (mem.Score > ret1)
                    //ret1の値を超える値が出てきた場合は値を更新
                    ret1 = mem.Score;
            }

            //Linq版--------------------------------------------------

            //membersの中でスコアが最大のものを抽出
            var ret2 = members.Max(x => x.Score);

            //表示する------------------------------------------------
            Console.WriteLine($"foreach:{ret1}, Linq:{ret2}");
        }

        /// <summary>
        /// Min関数のテスト
        /// </summary>
        static void MinTest()
        {
            //<Scoreの最小値を集計します。>
            //foreach版-----------------------------------------------

            //最小値を格納する変数に、int型の上限値を入れて初期化
            int ret1 = int.MaxValue;

            foreach (var mem in members)
            {
                //ret1の値よりも小さい値が出てきた場合は値を更新
                if (mem.Score < ret1)
                    ret1 = mem.Score;
            }

            //Linq版--------------------------------------------------

            //membersの中でスコアが最小のものを抽出
            var ret2 = members.Min(x => x.Score);

            //表示する------------------------------------------------
            Console.WriteLine($"foreach:{ret1}, Linq:{ret2}");
        }

        /// <summary>
        /// Aggregate関数のテスト
        /// </summary>
        static void AggregateTest()
        {
            //<Scoreの積算を取得する>
            //foreach版-----------------------------------------------

            //積算合計を保持する変数
            int ret1 = 1;
            
            foreach (var mem in members)
                //値を積算していく。
                ret1 = ret1 * mem.Score;

            //Linq版--------------------------------------------------

            //memberのScoreを順次積算していく。
            var ret2 = members.Aggregate(1,(n, next) => n * next.Score);

            //表示する------------------------------------------------
            Console.WriteLine($"foreach:{ret1}, Linq:{ret2}");
        }

        /// <summary>
        /// Aggregate関数のテスト2
        /// </summary>
        static void AggregateTest2()
        {
            //<最高スコアの人を名前付きで出力する>
            //for版-----------------------------------------------

            //最高スコア
            int maxScore = 0;
            //リスト検索用インデックス
            int index = 0;

            for(int i = 0; i < members.Count ; i++)
            {
                //MAXを求める。
                if (members[i].Score > maxScore)
                {
                    //最大値が見つかったら更新し、インデックスを保持。
                    maxScore = members[i].Score;
                    index = i;
                }
            }

            string ret1 = $"最高点 : {maxScore}点 / { members[index].Name }";

            //Linq版--------------------------------------------------

            //membersの中からスコアが最大のものを取得し、名前と一緒に表示する
            var ret2 = members.Aggregate(members.First(),
                                        //スコア最大を求める
                                        (max, next) => max.Score < next.Score ? next : max,
                                        //表示結果を作成する
                                        x => $"最高点 : {x.Score}点 / {x.Name }");

            //表示する
            Console.WriteLine($"foreach:{ret1}\nLinq:{ret2}");
        }

        #endregion
    }
}
