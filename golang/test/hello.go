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
	gofunc()

	fmt.Println("defer、panic、recover--------------------------------")
	//defer:在函式 return 前執行，而且一定會被執行
	/*
		就某些意義來說，defer 的角色類似於例外處理機制中 finally 的機制，畢竟Go沒有例外處理機制
		將資源清除的函式，藉由 defer 來處理，一方面大概也是為了在程式碼閱讀上，強調出資源清除的重要性
	*/
	//panic:中斷函式的流程
	/*
		如果在函式中執行 panic，那麼函式的流程就會中斷，若 A 函式呼叫了 B 函式，而 B 函式中呼叫了 panic，
		那麼 B 函式會從呼叫了 panic 的地方中斷，而 A 函式也會從呼叫了 B 函式的地方中斷，若有更深層的呼叫鏈，panic 的效應也會一路往回傳播。
		（如果你有例外處理的經驗，這就相當於被拋出的例外都沒有處理的情況。）
	*/
	//recover
	/*
		如果發生了 panic，而你必須做一些處理，可以使用 recover，這個函式必須在被 defer 的函式中執行才有效果，
		若在被 defer 的函式外執行，recover 一定是傳回 nil。
		如果有設置 defer 函式，在發生了 panic 的情況下，被 defer 的函式一定會被執行，若當中執行了 recover，
		那麼 panic 就會被捕捉並作為 recover 的傳回值，那麼 panic 就不會一路往回傳播，除非你又呼叫了 panic。
	*/

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
