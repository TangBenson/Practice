package main

import "fmt"

/*
在 Go 裡面有兩種資料結構可以處理包含許多元素的清單資料，分別是 Array 和 Slice：
Array：清單的長度是固定的（fixed length），屬於原生型別（primitive type），較少在程式中使用。
Slice：可以增加或減少清單的長度，使用 [] 定義，例如，[]byte 是 byte slice，指元素為 byte 的 slice；[]string 是 string slice，指元素為 string 的 slice。
不論是 Array 或 Slice，它們內部的元素都必須要有相同的資料型別（字串、數值、...）。
「陣列」一般指稱的是可變動長度的 Slice。

Capacity：從 slice 的第一個元素開始算起，它底層 array 的元素數目
Length：該 slice 中的元素數目
*/

func goarray() {
	//Array
	var arr0 [3]int
	arr0[0] = 35
	fmt.Println(len(arr0), arr0, cap(arr0))
	arr1 := [3]int{5, 6, 7}
	arr2 := [...]int{4, 5, 6, 7, 8, 9} //編譯器自動判斷數量
	fmt.Println(arr1, arr2)

	//Slice
	var sli1 []string //建立空slice
	// list[0] = "潔妮茲" //這樣不行耶，IDE卻沒跳錯，尼瑪的....
	sli1 = append(sli1, "潔妮茲")
	sli1 = append(sli1, "龍捲風")
	fmt.Println(len(sli1), sli1, cap(sli1))
	sli2 := []int{91, 92, 90, 94, 93, 95} //編譯器自動判斷數量
	sli2 = append(sli2, 96)
	sli3 := make([]int, 4) //建立空slice，適合用會對 slice 中特定位置元素進行操作時
	sli3 = append(sli3, 2023)
	sli4 := make([]string, 0, 5) // 建立空slice，大概知道需要多少元素時使用，搭配 append 使用
	sli4 = append(sli4, "惡靈餓三")
	fmt.Println(sli2)
}
