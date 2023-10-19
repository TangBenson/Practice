package main

import "fmt"

func forloop() {
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
}
