package main

import "fmt"

func gofunc() {
	fmt.Println("Function進階---------------------------------------")
	//函式作為值
	t1 := f1
	fmt.Println(t1(10))
	//函式變數宣告一
	var t2 func(int) int
	t2 = f1
	fmt.Println(t2(10))
	//函式變數宣告二
	type BiFunc = func(int) int //定義新型態 (Go 1.9版之前)
	var t3 BiFunc
	t3 = f1
	fmt.Println(t3(10))
	//函式變數宣告二
	type BiFunc2 func(int) int //型態別名宣告 (Go 1.9版之後)，BiFunc2 只是 func(int, int) int 的另一個名稱，而不是新的型態
	var t4 BiFunc2
	t4 = f1
	fmt.Println(t4(10))
	fmt.Println(&t4) //函式變數既然是個變數，也就可以對它取指標
	//函式當作值傳遞(我用匿名函式)
	data := []int{1, 2, 3, 4, 5, 6, 7, 8, 9, 10}
	fmt.Println(filter(data, func(n int) bool {
		return n > 7
	}))
	//函式中建立匿名函式，並將之傳回
	fmt.Println(f2()(2)) //12

	fmt.Println("Function進階-閉包------------------------------------")
	/*
		閉包本質上就是一個匿名函式，sum 變數被匿名函式包覆並傳入 forEach 之中，
		閉包將變數本身關閉在自己的範疇中，而不是變數的值
	*/
	numbers := []int{1, 2, 3, 4, 5}
	sum := 0
	MyforEach(numbers, func(elem int) {
		sum += elem
	})
	fmt.Println(sum)
	/*
		對 x_getter_setter 來說，x 參數也是變數，x_getter_setter 傳回了兩個匿名函式，這兩個匿名函式都形成了閉包，
		將 x 變數關閉在自己的範疇中，因此，你使用了 setX(20) 改變了 x 的值，使用 getX() 時取得的值，就會是修改後的值。
	*/
	getX, setX := x_getter_setter(10)
	fmt.Println(getX()) // 10
	setX(20)
	fmt.Println(getX()) // 20
}

type Getter = func() int
type Setter = func(int)

func x_getter_setter(x int) (Getter, Setter) {
	fmt.Printf("the parameter :\tx (%p) = %d\n", &x, x)
	getter := func() int {
		fmt.Printf("getter invoked:\tx (%p) = %d\n", &x, x)
		return x
	}
	setter := func(n int) {
		x = n
		fmt.Printf("setter invoked:\tx (%p) = %d\n", &x, x)
	}
	return getter, setter
}

type Consumer = func(int)

func MyforEach(elems []int, consumer Consumer) {
	for _, elem := range elems {
		consumer(elem)
	}
}

type Predicate = func(int) bool

func filter(origin []int, predicate Predicate) []int {
	filtered := []int{}
	for _, elem := range origin {
		if predicate(elem) {
			filtered = append(filtered, elem)
		}
	}
	return filtered
}

type Func1 = func(int) int

func f2() Func1 {
	/*
		f2 的 x 區域變數被閉包包覆，因此，你執行傳回的函式時，即使 f2 已執行完畢，
		x 變數依然是存活著在傳回的閉包範疇中，所以，你指定的引數總是會與 x 的值進行相加。
	*/
	x := 10
	return func(n int) int {
		return x + n
	}
}
