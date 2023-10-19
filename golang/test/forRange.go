package main

import "fmt"

func frange() {
	arr4 := [...]int{9, 4, 2}
	for index, elememt := range arr4 {
		fmt.Println(`index:`, index, `elememt:`, elememt)
	}
	//使用 _ 忽略傳回的值
	for _, elememt := range arr4 {
		fmt.Println(`elememt:`, elememt)
	}
}
