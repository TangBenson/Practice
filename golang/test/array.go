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

Arrays v.s Slices
1.大小固定 vs. 大小動態：
  Array：數組的大小是固定的，一旦創建，它不能改變。你需要在聲明數組時指定其大小。
  Slice：切片的大小是動態的，它可以動態增長或縮小。你可以在運行時改變切片的大小。
2.值拷貝 vs. 引用拷貝：
  Array：當你將一個數組賦值給另一個數組，它們的值將被複製，這意味著它們是獨立的。
  Slice：切片是對底層數組的引用。當你將一個切片賦值給另一個切片，它們引用的是相同的數組，這意味著它們共享數據。
3.長度和容量：
  Array：數組的長度是固定的，不能更改。
  Slice：切片有兩個重要的屬性，即長度(len)和容量(cap)。切片的長度表示當前切片中的元素數量，容量表示底層數組中的元素數量，切片可以動態增長，但不超過容量。
4.初始化：
  Array：數組的初始化通常需要指定大小和初始值。
  Slice：切片通常通過從現有數組或其他切片中創建，或者使用內建的 make 函數來初始化。
5.適用場景：
  Array：通常用於需要固定大小的數據集合，例如代表一周中的日期的數組。
  Slice：更靈活，通常用於處理動態數據集合，例如處理HTTP請求的參數或持久性存儲中的記錄。
總之，Slice是Go中更為常見和靈活的數據結構，而Array用於具有固定大小的數據集合。當需要動態增長的數據結構時，通常首選Slice。
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
