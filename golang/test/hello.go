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
	numbers := []int{1, 2, 3, 4, 5}
	fmt.Println(Sum(numbers...)) // 15
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
	// dog := Dog{Name: "Kenny"}
	// ShowEat(&dog)
	// ShowRun(&dog)

	fmt.Println("map鍵值對---------------------------------------")
	gomap()
}

func show(max int) {
	var result int = 0
	var n int
	for n = 1; n < max; n++ {
		result += n
	}
	fmt.Println(result)
}

type Animal interface {
	Eat()
	Run()
}

type Dog struct {
	Name string
}

func (d *Dog) Eat() {
	fmt.Printf("%s is eating\n", d.Name)
}

func (d *Dog) Run() {
	fmt.Printf("%s is running\n", d.Name)
}

func ShowEat(animal Animal) {
	animal.Eat()
}

func ShowRun(animal Animal) {
	animal.Run()
}
