package main //可執行程式必須使用 main 封包，若是寫套件就不需要用 main
import "fmt" //載入內建的 fmt 封包，做輸出輸入

func main() {
	// 程式進入點
	fmt.Println("嗨~寶貝", 1314520, true)

	fmt.Println("宣告變數 & f-string-----------------------------")

	var xt int
	xt = 4 * 8 // xt="sss"會錯
	fmt.Println(xt)
	var f1 float64 = 3.1415
	fmt.Println(f1)
	var s1 string = "字串ㄟㄟㄟㄟ"
	fmt.Println(s1)
	var b bool = true
	fmt.Println(b)
	var c rune = 'b'
	fmt.Println(c)
	var name string = "包子"
	age := 34 // :=是語法糖，省略型別宣告
	result2 := `My name is ` + name + ` and I am ` + fmt.Sprint(age) + ` years old.`
	fmt.Println(result2)

	fmt.Println("使用者輸入-----------------------------")

	fmt.Println("請輸入數字")
	var x1 int
	fmt.Scanln(&x1) //&變數名稱:取得變數的指標(Pointer)
	fmt.Println(x1)
	fmt.Println("請輸入2個數字，用空白隔開")
	var x2 int
	var y int
	fmt.Scanln(&x2, &y)
	var result int = x2 + y
	fmt.Println(result)

	fmt.Println("條件式 & 迴圈-----------------------------")

	if 100 == 99 {
		fmt.Println('Q')
	} else {
		fmt.Println('W')
	}
	// Golang只有for迴圈
	var q int
	for q = 0; q < 3; q++ {
		fmt.Println("AAA")
	}

	fmt.Println("Function呼叫-----------------------------")

	Show(10)
	var xx int = Mutiply(3, 4)
	yy := Mutiply(3, 4)
	fmt.Println(xx)
	fmt.Println(yy)
	var x3 int
	var s2 string
	x3, s2 = Test(5, 2)
	fmt.Println(x3, s2)
	Test2()

	fmt.Println("Pointer指標變數-----------------------------")

	// Pointer用來存記憶體位置
	var x int = 3
	fmt.Println(&x)    //取得記憶體位置
	var xPtr *int = &x //建立Pointer指標變數
	fmt.Println(xPtr)
	fmt.Println(*xPtr) //反解Pointer

	fmt.Println("struct宣告-----------------------------")

	var p1 Person = Person{"柔柔", 18}
	fmt.Println(p1.age, p1.name)
}
