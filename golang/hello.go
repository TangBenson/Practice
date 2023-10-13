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
	Show(10)
	var xx int = Mutiply(3, 4)
	fmt.Println(xx)
	var sq int
	var sw string
	sq, sw = Test(5, 2)
}

func Show(max int) {
	var result int = 0
	var n int
	for n = 1; n < max; n++ {
		result += n
	}
	fmt.Println(max)
}

func Mutiply(n1 int, n2 int) int {
	var result int = n1 * n2
	return result
}

func Test(n1 int, n2 int) (int, string) {
	return n1 * n2, "ddd"
}
