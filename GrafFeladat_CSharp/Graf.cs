using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok){
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++){
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2){
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama){
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek){
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2){
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }
        public void Torles(int cs1, int cs2){
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama){
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }


            int i = 0;
            int ind1 = -1;
            int ind2 = -1;
            foreach (var el in elek){
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2){
                    ind1 = i;
                }
                else if (el.Csucs2 == cs1 && el.Csucs1 == cs2){
                    ind2 = i;
                }
                i++;
            }

            if (ind2 != -1){
                elek.RemoveAt(ind2);
            }
            if (ind1 != -1){
                elek.RemoveAt(ind1);
            }

        }
        public override string ToString(){
            string str = "Csucsok:\n";
            foreach (var cs in csucsok){
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek){
                str += el + "\n";
            }
            return str;
        }
        public void szelessegiBejar(int kezdoPont){
            List<int> bejar = new List<int>();

            Queue<int> sor = new Queue<int>();
            sor.Enqueue(kezdoPont);
            bejar.Add(kezdoPont);

            while (sor.Count > 0){
                int k = sor.Dequeue();

                Console.WriteLine("A csúcs: " + k);

                foreach (var item in elek){
                    if (item.Csucs1 == k && !(bejar.Contains(item.Csucs2))){
                        sor.Enqueue(item.Csucs2);
                        bejar.Add(item.Csucs2);
                    }
                }

            }
        }
        public void melysegiBejar(int kezdoPont){
            List<int> bejar = new List<int>();

            Stack<int> kovetkezok = new Stack<int>();
            kovetkezok.Push(kezdoPont);
            bejar.Add(kezdoPont);

            while (kovetkezok.Count > 0){
                int k = kovetkezok.Pop();

                Console.WriteLine("A csúcs: " + k);

                foreach (var item in elek){
                    if (item.Csucs1 == k && !(bejar.Contains(item.Csucs2))){
                        kovetkezok.Push(item.Csucs2);
                        bejar.Add(item.Csucs2);
                    }
                }
            }
        }
    }
}
