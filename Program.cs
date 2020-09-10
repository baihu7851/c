using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace c練習
{
    class Program
    {
        #region 6-1 符號轉換
        static string SymbolChange(string input)
        {
            return input.Replace(">", "&gt")
                        .Replace("<", "&lt")
                        .Replace(@"\r\n", "<br>")
                        .Replace("|", "&brvbar");
        }
        #endregion

        #region 6-2 數字確認
        static string NumberCheck(string input)
        {
            if (Regex.IsMatch(input, @"^[-]?[\d][.]?[\d]?"))
            {
                return input;
            }
            else
            {
                return "-1";
            }
        }
        #endregion

        #region 10-3 Email確認
        static string EmailCheck(string input)
        {
            try
            {
                MailAddress email = new MailAddress(input);
                return input;
            }
            catch
            {
                return "-1";
            }
        }
        #endregion

        #region 10-4 手機號碼確認
        static string PhoneCheck(string input)
        {
            input = input.Replace("-", "").Replace(" ", "").Replace("+", "").Replace("_", "");
            if (Regex.IsMatch(input, @"^09[0-9]{8}$") || Regex.IsMatch(input, @"^8869[0-9]{8}$"))
            {
                return input;
            }
            else
            {
                return "-1";
            }
        }
        #endregion

        #region 10-5 身分證確認
        static string IdCheck(string input)
        {
            input = input.ToUpper();
            if (Regex.IsMatch(input, "^[A-Z]{1}[1-2]{1}[0-9]{8}$"))
            {
                int cityNumber = 0, idNumber = 0;
                int count = 0;
                string[] city = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" };
                int s = Array.IndexOf(city, input.Substring(0, 1));
                s += 10;
                cityNumber = (((s % 10) * 9) + (s / 10));
                for (int i = 1; i < 9; i++)
                {
                    idNumber += (Convert.ToInt32(input.Substring(i, 1)) * (9 - i));
                }
                count = 10 - ((cityNumber + idNumber) % 10);
                if (count == Convert.ToInt32(input.Substring(9, 1)))
                {
                    return input;
                }
                else
                {
                    return "身分證字號不存在";
                }
            }
            else
            {
                return "-1";
            }
        }
        #endregion

        #region 10-6 超過省略
        private static string Omit(string input)
        {
            int con = 0;
            do
            {
                try
                {
                    Console.WriteLine("輸入最大字數限制");
                    con = Convert.ToInt32(Console.ReadLine());
                }
                catch { }
            } while (con <= 0);
            int i = Convert.ToInt32(con);
            if (input.Length >= i)
            {
                input = input.Substring(0, i);
                return $"{input}" + "．．．";
            }
            else
            {
                return input;
            }
        }
        #endregion
        #region 10-7 西元轉民國(年月日間需有分別 ex:"/",".","-")
        static string TaiwanTimeChange(string input)
        {
            try
            {
                DateTime date = Convert.ToDateTime(input);
                TaiwanCalendar taiwanCalendar = new TaiwanCalendar();
                return $"民國 {taiwanCalendar.GetYear(date)}年 {date.Month}月 {date.Day}日";
            }
            catch
            {
                return "-1";
            }
        }
        #endregion

        #region 10-8 西元轉民國和星期(年月日間需有分別 ex:"/",".","-")
        static string TaiwanDay(string input)
        {
            if (TaiwanTimeChange(input) != "-1")
            {
                Console.Write(TaiwanTimeChange(input));
                DateTime date = Convert.ToDateTime(input);
                string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                return weekdays[Convert.ToInt32(date.DayOfWeek)];
            }
            else
            {
                return "-1";
            }

        }
        #endregion

        #region 10-9 閏年判斷
        static string LeapYear(string input)
        {
            if (Regex.IsMatch(input, @"[\d]"))
            {
                int year = Int32.Parse(input);
                if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                {
                    return "閏年";
                }
                else
                {
                    return "非閏年";
                }
            }
            return "-1";
        }
        #endregion

        #region 10-10 今日運勢
        static string Fortune(string input)
        {
            if (PhoneCheck(input) != "-1")
            {
                string fortune = @" 0 有點特殊..........大吉
                                1 大展鴻圖．可獲成功 吉
                                2 一盛一衰．勞而無功 凶
                                3 蒸蒸日上．百事順遂 吉
                                4 坎坷前途．苦難折磨 凶
                                5 生意欣榮．名利雙收 吉
                                6 天降幸運．可成大功 吉
                                7 和氣致祥．必獲成功 吉
                                8 貫徹志望．成功可期 吉
                                9 獨營無力．財利無望 凶
                                10 空費心力．徒勞無功 凶
                                11 穩健著實．必得人望 吉
                                12 薄弱無力．謀事難成 凶
                                13 天賦吉運．能得人望 吉
                                14 是成是敗．惟靠堅毅 凶
                                15 大事成就．一定興隆 吉
                                16 成就大業．名利雙收 吉
                                17 有貴人助．可得成功 吉
                                18 順利昌隆．百事亨通 吉
                                19 內外不合．障礙重重 凶
                                20 歷盡艱難．焦心憂勞 凶
                                21 專心經營．善用智能 吉
                                22 懷才不遇．事不如意 凶
                                23 名顯四方．終成大業 吉
                                24 須靠自力．能奏大功 吉
                                25 天時地利．再得人格 吉
                                26 波瀾起伏．凌駕萬難 凶
                                27 一盛一衰．可守成功 凶帶吉
                                28 遇衰轉吉．遇厄轉好 吉
                                29 青雲直上．才略奏功 吉
                                30 吉凶參半．得失相伴 凶
                                31 名利雙收．大業成就 吉
                                32 池中之龍．成功可望 吉
                                33 智能慎始．必可昌隆 吉
                                34 災難不絕．難望成功 凶
                                35 中吉之數．進退保守 吉
                                36 波瀾重疊．常陷窮困 凶
                                37 逢凶化吉．風調雨順 吉
                                38 名雖可得．利則難獲 凶帶吉
                                39 光明坦途．指日可待 吉
                                40 一盛一衰．浮沉不定 吉帶凶
                                41 天賦吉運．前途無限 吉
                                42 事業不專．十九不成 吉帶凶
                                43 忍耐自重．轉凶為吉 吉帶凶
                                44 事難遂願．貪功好進 凶
                                45 綠葉發枝．一舉成名 吉
                                46 坎坷不平．艱難重重 凶
                                47 有貴人助．可成大業 吉
                                48 名利俱全．繁榮富貴 吉
                                49 遇吉則吉．遇凶則凶 凶
                                50 吉凶互見．一成一敗 吉帶凶
                                51 一盛一衰．浮沉不常 吉帶凶
                                52 雨過天青．即獲成功 吉
                                53 盛衰參半．先吉後凶 吉帶凶
                                54 雖傾全力．難望成功 凶
                                55 外觀隆昌．內隱禍患 吉帶凶
                                56 事與願違．終難成功 凶
                                57 努力經營．時來運轉 吉
                                58 浮沉多端．始凶終吉 凶帶吉
                                59 遇事猶疑．難望成事 凶
                                60 心迷意亂．難定方針 凶
                                61 雲遮半月．百隱風波 吉帶凶
                                62 煩悶懊惱．事事難展 凶
                                63 萬物化育．繁榮之象 吉
                                64 十九不成．徒勞無功 凶
                                65 吉運自來．能享盛名 吉
                                66 內外不和．信用缺乏 凶
                                67 事事如意．富貴自來 吉
                                68 不失先機．可望成功 吉
                                69 動搖不安．常陷逆境 凶
                                70 慘澹經營．難免貧困 凶
                                71 吉凶參半．惟賴勇氣 吉帶凶
                                72 得而復失．難以安順 凶
                                73 安樂自來．自然吉祥 吉
                                74 如無智謀．難望成功 凶
                                75 吉中帶凶．進不如守 吉帶凶
                                76 此數大凶．破產之象 凶
                                77 先苦後甘．不致失敗 吉帶凶
                                78 有得有失．華而不實 吉帶凶
                                79 前途無光．希望不大 凶";
                double phoneNumber = Convert.ToDouble(input.Substring(input.Length - 4, 4)) / 80;
                phoneNumber -= Math.Floor(phoneNumber);
                phoneNumber *= 80;
                //低版本使用另外兩種code
                string[] tmp = fortune.Split("\n");
                //string[] tmp = fortune.Split('\n');
                //string[] tmp = Regex.Split(fortune,"\n");
                string ans = tmp[Convert.ToInt32(phoneNumber)];
                ans = ans.Trim();
                int i = ans.IndexOf(" ");
                return ans.Substring(i + 1);
            }
            return "-1";
        }
        #endregion

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("輸入判斷物");
                string i = Convert.ToString(Console.ReadLine());
                while (i == "")
                {
                    Console.WriteLine("讀入空值請重新輸入");
                    i = Convert.ToString(Console.ReadLine());
                }
                //更改使用的副程式
                string ans = Fortune(i);
                //預設為手機號碼判斷運勢
                if (ans == "-1")
                {
                    Console.WriteLine("格式錯誤");
                }
                else
                {
                    Console.WriteLine(ans);
                }
                Console.ReadKey();
            }
        }
    }
}