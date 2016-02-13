using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesweeper_voicehovich {
    [Serializable()]
    public class serializableArrays {

        public Tuple<int, string>[] begBestScores = new Tuple<int, string>[10];
        public Tuple<int, string>[] interBestScores = new Tuple<int, string>[10];
        public Tuple<int, string>[] advBestScores = new Tuple<int, string>[10];


        private static String filePath = "bestScores.txt";

        public serializableArrays() {

            for (int i = 0; i < 10; i++) {
                begBestScores[i] = new Tuple<int, string>(999, "unknown");
                interBestScores[i] = new Tuple<int, string>(999, "unknown");
                advBestScores[i] = new Tuple<int, string>(999, "unknown");
            }

        }


        public static void Save(serializableArrays obj) {

            System.IO.Stream stream = System.IO.File.Open(filePath, System.IO.FileMode.Create);
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, obj);

        }

        public static serializableArrays Load() {
            try {
                System.IO.Stream stream = System.IO.File.Open(filePath, System.IO.FileMode.Open);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (serializableArrays)binaryFormatter.Deserialize(stream);
            }
            #pragma warning disable 0168//disable variable never used warning
            catch (System.Runtime.Serialization.SerializationException se) {
                serializableArrays sa = new serializableArrays();
                return sa;
            }
            #pragma warning disable 0168
            catch (System.IO.FileNotFoundException e) {
                serializableArrays sa = new serializableArrays();
                return sa;
            }
        }



        public void manageScores(String difficulty, int score) {
            if (difficulty.Equals("beginner")) {
                updateScores(begBestScores, score);
            }
            else if (difficulty.Equals("intermediate")) {
                updateScores(interBestScores, score);
            }
            else if (difficulty.Equals("advanced")) {
                updateScores(advBestScores, score);
            }
        }

        public void updateScores(Tuple<int, string>[] arrToUpdate, int score) {

            for (int i = 0; i < 10; i++) {
                if (arrToUpdate[i].Item1 > score) {

                    for (int j = 9; j > i; j--) {
                        arrToUpdate[j] = arrToUpdate[j - 1];
                    }


                    Label aCaseForName = new Label();
                    WinnerNameForm form = new WinnerNameForm(aCaseForName);
                    form.ShowDialog();

                    arrToUpdate[i] = new Tuple<int, string>(score, aCaseForName.Text);
                    serializableArrays.Save(this);
                    return;
                }
            }
        }

    }
}