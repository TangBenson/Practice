package main //可執行程式必須使用 main 封包，若是寫套件就不需要用 main

import (
	"fmt" //載入內建的 fmt 封包，做輸出輸入
	myfunc "mymod/funcs"
)

func main() {
	// 程式進入點
	fmt.Println("嗨~寶貝", 1314520, true)

	fmt.Println("宣告變數 & f-string-----------------------------")

	var xt int
	xt = 4 * 8 // xt="sss"會錯
	var f1 float64 = 3.1415
	var s1 string = "字串ㄟㄟㄟㄟ"
	var b bool = true
	var c rune = 'b'
	fmt.Println(xt, f1, s1, b, c)
	age := 34 // :=是語法糖，省略型別宣告
	result2 := `My name is ` + s1 + ` and I am ` + fmt.Sprint(age) + ` years old.`
	fmt.Println(result2)

	fmt.Println("使用者輸入--------------------------------------")

	fmt.Println("請輸入數字")
	var x1 int
	fmt.Scanln(&x1)                                         //&變數名稱:取得變數的指標(Pointer)
	fmt.Println("你輸入的數字對應的Pointer:", &x1, "  ,你輸入的數字:", x1) //&變數名稱:取得變數的指標(Pointer)
	fmt.Println("請輸入2個數字，用空白隔開")
	var x2 int
	var y int
	fmt.Scanln(&x2, &y)
	var result int = x2 + y
	fmt.Println("兩數相加為:", result)

	fmt.Println("條件式 & 迴圈-----------------------------------")

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

	fmt.Println("Function呼叫-----------------------------------")

	show(10)
	var x3 int
	var s2 string
	x3, s2 = myfunc.Test(5, 2)
	fmt.Println(x3, s2)
	myfunc.Test2()

	fmt.Println("Pointer指標變數--------------------------------")

	// Pointer用來存記憶體位置
	var x int = 3
	fmt.Println(&x)    //取得記憶體位置
	var xPtr *int = &x //建立Pointer指標變數
	fmt.Println(xPtr)
	fmt.Println(*xPtr) //反解Pointer

	fmt.Println("struct宣告-------------------------------------")

	var p1 myfunc.Person = myfunc.Person{
		Name: "fff",
		Age:  58,
	}
	fmt.Println(p1.Age, p1.Name)

	fmt.Println("陣列-------------------------------------------")

	var scores [3]int
	scores[0] = 35
	fmt.Println(len(scores), scores)
	arr1 := [3]int{5, 6, 7}
	arr2 := [...]int{4, 5, 6, 7, 8, 9} //編譯器自動判斷數量
	arr3 := []int{4, 5, 6, 7, 8, 9}    //編譯器自動判斷數量
	fmt.Println(arr1, arr2, arr3)

	fmt.Println("for range--------------------------------------")

	arr4 := [...]int{9, 4, 2}
	for index, elememt := range arr4 {
		fmt.Println(`index:`, index, `elememt:`, elememt)
	}
	//使用 _ 忽略傳回的值
	for _, elememt := range arr4 {
		fmt.Println(`elememt:`, elememt)
	}

	fmt.Println("interface接口-----------------------------------")

	dog := Dog{Name: "Kenny"}
	ShowEat(&dog)
	ShowRun(&dog)
}

func show(max int) {
	var result int = 0
	var n int
	for n = 1; n < max; n++ {
		result += n
	}
	fmt.Println(result)
}
