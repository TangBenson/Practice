using System;

/*
static的成員不需要實體(Instance)就能進行訪問
static的成員是大家共享的 , 就算宣告了不同的物件 , 靜態變數仍然是共享的

非靜態的成員必須 new 一個實體(Instance)才能進行訪問

靜態並非沒有實體 , 而是只有一個實體 , 在程式執行之初就建立 , 並佔用記憶體位置 , 
而且一直存在 , 當程式用上一堆靜態成員的時候就造成不必要的記憶體浪費

靜態方法屬於類別所有，只能以類別名叫用
非靜態方法屬於實體所有，只能以實體叫用

靜態類別內無法有非靜態方法(靜態類別內一切都是靜態的，不僅限方法)
靜態類別僅包含靜態成員
靜態類別無法被繼承

類別、類別資料成員們(方法、屬性、欄位、建構函式)、運算子、事件 , 可搭配使用

<結論>
static method:不須new出物件即可使用，直接透過class名稱存取
static變數:直接透過class名稱存取、所有物件共用此變數
static class:無法new出物件
*/
namespace CsharpStudy;
class StaticTang
{
    public static class StaticClass
    {
        // private int ID = 0; //錯誤:靜態類別內無法有非靜態成員
        private static int _id = 98;
        public static int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        // public void Go() { } //錯誤:靜態類別內無法有非靜態方法
        public static string Go() => $"這是StaticClass的靜態方法 - {_id}";
    }
    public class NonStaticClass
    {
        private int _id = 10;
        private static int _id2 = 10;
        public int ID { get; set; }
        public static int ID2
        {
            get { return _id2; }
            set { _id2 = value; }
        }

        public string Go() => $"這是NonStaticClass的非靜態方法 - {_id}";
        public static string Go2() => $"這是NonStaticClass的靜態方法 - {_id2}";
    }
    static void Main(string[] args)
    {
        // // StaticClass ss = new(); //錯誤:靜態類別無法new出物件
        // StaticClass.ID = 10;
        // Console.WriteLine(StaticClass.ID);
        // Console.WriteLine(StaticClass.Go());


        // Console.WriteLine(NonStaticClass.ID); //錯誤
        Console.WriteLine(NonStaticClass.ID2);
        NonStaticClass.ID2 = 100;
        Console.WriteLine(NonStaticClass.ID2);
        // Console.WriteLine(NonStaticClass.Go()); //錯誤
        Console.WriteLine(NonStaticClass.Go2());
        Console.WriteLine("=====================================");
        NonStaticClass a = new();
        NonStaticClass b = new();
        //網路文章說一個設1則全部都1，我聽他放屁....
        a.ID = 50;
        // a.ID2 = 50; //錯誤
        Console.WriteLine(a.ID);
        Console.WriteLine(b.ID);
        Console.WriteLine(a.Go());
    }
}

/*
從執行效能來看 靜態方法 比較好 因為是共用的 省去建立 跟 銷毀 物件的效能
但是也代表 靜態方法 永遠存在 不論這方法 會不會被執行

即使這樣說也很難判斷到底何時要用 靜態方法 何時不用
但是如果從物件導向的角度 - 封裝 出發 就很明確

例如一個公開的類別的 公開靜態方法 跟 公開非靜態方法 就有意義上的不同
公開的類別的公開靜態方法 即使不屬於此類別 也能叫用 因為都是公開的
公開的類別的公開非靜態方法 卻只能是 屬於此類別的 實體 才能叫用

例如一個公開類別名叫 男人 有個方法叫 站著小便
如果他是 非靜態方法 那麼可以確定 只有男人可以站著小便
如果他是 靜態方法 那就好玩了 就算是女人類別 也能 執行 男人.站著小便() 方法
好吧我知道這例子很鳥 或許真的有女人喜歡站著小便?!

那再舉一例吧 跟錢有關的 都比較有深刻體驗

例如一個公開類別名叫 我 有個方法叫 花光我的積蓄當月光仙子
如果他是 非靜態方法 那麼可以確定 只有我可以花光我的積蓄當月光仙子
如果他是 靜態方法 那就好玩了 就算是路人類別 也能 執行 我.花光我的積蓄當月光仙子() 方法
然後我的薪水就被花光了 而且花光的人 還不是我

WTF，怎麼每個聽起來都很可怕 那又什麼時候該用 靜態方法?
例如一個公開類別名叫 學生 有個方法叫 作弊
如果他是 靜態方法 那真棒 誰都可以幫助學生作弊
如果他是 非靜態方法 那有點糟 學生只能自立自強 備妥小抄望遠鏡

好吧我知道這例子很鳥 鼓吹作弊 違反社會善良風俗

例如一個公開類別名叫 我 有個方法叫 叫我起床
如果他是 靜態方法 那真棒　誰都可以叫我起床
如果他是 非靜態方法 那有點糟 只有我可以叫我自己起床 沒人幫的了我
*/