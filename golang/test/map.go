package main

import "fmt"

func gomap() {
	// map是成對鍵值資料結構

	friendlist := make(map[string]int) //建立空map
	// friendlist := map[string]int{} //同上
	fmt.Println(friendlist)      // map[]
	fmt.Println(len(friendlist)) // 0
	friendlist["雯婷"] = 2015
	friendlist["皓瑋"] = 2014
	fmt.Println(friendlist)       // map[雯婷:2015 皓瑋:2014]
	fmt.Println(len(friendlist))  // 2
	fmt.Println(friendlist["雯婷"]) // 2015
	fmt.Println(friendlist["皓瑋"]) // 2014

	friendlist2 := map[string]int{
		"皇上": 2018,
		"小辰": 2003}
	fmt.Println(friendlist2)

	v, exists := friendlist2["皇上"]
	fmt.Println(`v:`, v, `exist:`, exists)
	_, exists2 := friendlist2["小辰"]
	fmt.Println(`exist:`, exists2)
}
