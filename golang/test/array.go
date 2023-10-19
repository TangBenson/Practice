package main

import "fmt"

/*
在 Go 裡面有兩種資料結構可以處理包含許多元素的清單資料，分別是 Array 和 Slice：
Array：清單的長度是固定的（fixed length），屬於原生型別（primitive type），較少在程式中使用。
Slice：可以增加或減少清單的長度，使用 [] 定義，例如，[]byte 是 byte slice，指元素為 byte 的 slice；[]string 是 string slice，指元素為 string 的 slice。
不論是 Array 或 Slice，它們內部的元素都必須要有相同的資料型別（字串、數值、...）。
「陣列」一般指稱的是可變動長度的 Slice。
*/

func goarray() {
	var scores [3]int
	scores[0] = 35
	fmt.Println(len(scores), scores)

	var list []string
	// list[0] = "潔妮茲" //這樣不行耶，IDE卻沒跳錯，尼瑪的....
	list = append(list, "潔妮茲")
	list = append(list, "龍捲風")
	fmt.Println(len(list), list)

	arr1 := [3]int{5, 6, 7}
	arr2 := [...]int{4, 5, 6, 7, 8, 9}    //編譯器自動判斷數量
	arr3 := []int{91, 92, 90, 94, 93, 95} //編譯器自動判斷數量，但這是slice不是array喔
	// arr2 = append(arr2,9)//會錯
	arr3 = append(arr3, 96)
	fmt.Println(arr1, arr2, arr3)
}
