package main

import "fmt"

func Gcd(m, n int) int {
	if n == 0 {
		return m
	} else {
		return Gcd(n, m%n)
	}
}

func Show(max int) {
	var result int = 0
	var n int
	for n = 1; n < max; n++ {
		result += n
	}
	fmt.Println(result)
}

func Mutiply(n1 int, n2 int) int {
	var result int = n1 * n2
	return result
}

func Test(n1 int, n2 int) (int, string) {
	return n1 * n2, "ddd"
}

func Test2() {
	//defer 會等到周圍的 function 都執行完成後，再執行
	fmt.Println("counting")

	for i := 0; i < 10; i++ {
		defer fmt.Println(i)
	}

	fmt.Println("done")
}

type Person struct {
	name string
	age  int
}
