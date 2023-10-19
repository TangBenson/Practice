package main

import "fmt"

func gopointer() {
	// Pointer用來存記憶體位置
	var x int = 3
	fmt.Println(&x)    //取得記憶體位置
	var xPtr *int = &x //建立Pointer指標變數
	fmt.Println(xPtr)
	fmt.Println(*xPtr) //反解Pointer
}
