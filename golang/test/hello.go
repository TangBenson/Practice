package main //可執行程式必須使用 main 封包，若是寫套件就不需要用 main

import (
	"fmt" //載入內建的 fmt 封包，做輸出輸入
	"mymod/myfunc"
)

func main() {
	// 程式進入點

	// fmt.Println("嗨~寶貝", 1314520, true)

	// fmt.Println("Function呼叫-----------------------------------")
	// show(10)
	// sayHi()
	// var x3 int
	// var s2 string
	// x3, s2 = myfunc.Test(5, 2)
	// fmt.Println(x3, s2)
	// numbers := []int{1, 2, 3, 4, 5}
	// fmt.Println(myfunc.Sum(numbers...)) // 15
	myfunc.Test2()

	// fmt.Println("宣告變數 & f-string-----------------------------")
	// var xt int
	// xt = 4 * 8 // xt="sss"會錯
	// var f1 float64 = 3.1415
	// var s1 string = "字串ㄟㄟㄟㄟ"
	// var b bool = true
	// var c rune = 'b'
	// fmt.Println(xt, f1, s1, b, c)
	// age := 34 // :=是語法糖，省略型別宣告
	// result2 := `My name is ` + s1 + ` and I am ` + fmt.Sprint(age) + ` years old.`
	// fmt.Println(result2)

	// fmt.Println("使用者輸入--------------------------------------")
	// userinput()

	// fmt.Println("條件式 & 迴圈-----------------------------------")
	// forloop()

	// fmt.Println("Pointer指標變數--------------------------------")
	// gopointer()

	// fmt.Println("struct宣告-------------------------------------")
	// var p1 myfunc.Person = myfunc.Person{
	// 	Name: "fff",
	// 	Age:  58,
	// }
	// fmt.Println(p1.Age, p1.Name)

	// fmt.Println("陣列-------------------------------------------")
	// goarray()

	// fmt.Println("for range--------------------------------------")
	// frange()

	// fmt.Println("interface接口----------------------------------")
	// dog := myfunc.Dog{Name: "Kenny"}
	// myfunc.ShowEat(&dog)
	// myfunc.ShowRun(&dog)

	// fmt.Println("map鍵值對---------------------------------------")
	// gomap()

	fmt.Println("Function進階---------------------------------------")
	// //函式作為值
	// t1 := f1
	// fmt.Println(t1(10))
	// //函式變數宣告一
	// var t2 func(int) int
	// t2 = f1
	// fmt.Println(t2(10))
	// //函式變數宣告二
	// type BiFunc = func(int) int //定義新型態 (Go 1.9版之前)
	// var t3 BiFunc
	// t3 = f1
	// fmt.Println(t3(10))
	// //函式變數宣告二
	// type BiFunc2 func(int) int //型態別名宣告 (Go 1.9版之後)，BiFunc2 只是 func(int, int) int 的另一個名稱，而不是新的型態
	// var t4 BiFunc2
	// t4 = f1
	// fmt.Println(t4(10))
	// fmt.Println(&t4) //函式變數既然是個變數，也就可以對它取指標
	// //函式當作值傳遞(我用匿名函式)
	// data := []int{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
	// fmt.Println(filter(data, func(n int) bool {
	// 	return n > 7
	// }))
	// //函式中建立匿名函式，並將之傳回
	// fmt.Println(f2()(2)) //12
	//閉包
	numbers := []int{1, 2, 3, 4, 5}
	sum := 0
	MyforEach(numbers, func(elem int) {
		sum += elem
	})
	fmt.Println(sum) // 15

	numbers2 := []int{1, 2, 3, 4, 5}
	sum2 := 0
	for _, elem := range numbers2 {
		sum2 += elem
	}
	fmt.Println(sum2) // 15
}

type Consumer = func(int)

func MyforEach(elems []int, consumer Consumer) {
	for _, elem := range elems {
		consumer(elem)
	}
}
func show(max int) {
	var result int = 0
	var n int
	for n = 1; n < max; n++ {
		result += n
	}
	fmt.Println(result)
}
func f1(a int) int {
	return a + 97
}

type Predicate = func(int) bool

func filter(origin []int, predicate Predicate) []int {
	filtered := []int{}
	for _, elem := range origin {
		if predicate(elem) {
			filtered = append(filtered, elem)
		}
	}
	return filtered
}

type Func1 = func(int) int

func f2() Func1 {
	x := 10
	return func(n int) int {
		return x + n
	}
}
