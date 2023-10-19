package main

import "fmt"

func userinput() {
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
}
