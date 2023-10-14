package main //可執行程式必須使用 main 封包，若是寫套件就不需要用 main
import "fmt" //載入內建的 fmt 封包，做輸出輸入
func main() {
	// 程式進入點

	// fmt.Println("嗨~寶貝", 1314520, true)

	// var xt int
	// xt = 4 * 8
	// fmt.Println(xt)
	// // xt="sss" //會錯
	// var f float64 = 3.1415
	// fmt.Println(f)
	// var s string = "字串ㄟㄟㄟㄟ"
	// fmt.Println(s)
	// var b bool = true
	// fmt.Println(b)
	// var c rune = 'b'
	// fmt.Println(c)

	// fmt.Println("請輸入數字")
	// var x int
	// fmt.Scanln(&x) //&變數名稱:取得變數的指標(Pointer)
	// fmt.Println(x)

	// fmt.Println("請輸入2個數字，用空白隔開")
	// var x2 int
	// var y int
	// fmt.Scanln(&x2, &y)
	// var result int = x2 + y
	// fmt.Println(result)

	// fmt.Println("-----------------------------")

	// if 100 == 99 {
	// 	fmt.Println('Q')
	// } else {
	// 	fmt.Println('W')
	// }

	// fmt.Println("-----------------------------")

	// // Golang只有for迴圈
	// var q int
	// for q = 0; q < 3; q++ {
	// 	fmt.Println("AAA")
	// }

	// fmt.Println("-----------------------------")

	Show(10)
	var xx int = Mutiply(3, 4)
	yy := Mutiply(3, 4) // :=是語法糖，yy和xx一樣
	fmt.Println(xx)
	fmt.Println(yy)
	var x int
	var s string
	x, s = Test(5, 2)
	fmt.Println(x, s)
	Test2()

	// fmt.Println("-----------------------------")

	// Pointer用來存記憶體位置
	// var x int = 3
	// fmt.Println(&x)    //取得記憶體位置
	// var xPtr *int = &x //建立Pointer指標變數
	// fmt.Println(xPtr)
	// fmt.Println(*xPtr) //反解Pointer

	// fmt.Println("-----------------------------")

	// var p1 Person = Person{"柔柔", 18}
	// fmt.Println(p1.age, p1.name)
}

func Show(max int) {
	var result int = 0
	var n int
	for n = 1; n < max; n++ {
		result += n
	}
	fmt.Println(result)
}

func Mutiply(n1 int, n2 int) int {
	var result int = n1 * n2
	return result
}

func Test(n1 int, n2 int) (int, string) {
	return n1 * n2, "ddd"
}

func Test2() {
	//defer 會等到周圍的 function 都執行完成後，再執行
	fmt.Println("counting")

	for i := 0; i < 10; i++ {
		defer fmt.Println(i)
	}

	fmt.Println("done")
}

type Person struct {
	name string
	age  int
}
