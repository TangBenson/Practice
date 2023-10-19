package main

import (
	"encoding/json"
	"fmt"
	"log"
	"math/rand"
	"net/http"
	"strconv"

	"github.com/gorilla/mux"
)

type Movie struct {
	ID       string    `json:"id"`
	Isbn     string    `json:"isbn"`
	Title    string    `json:"title"`
	Director *Director `json:"director"`
}

type Director struct {
	Firstname string `json:"firstname"`
	Lastname  string `json:"lastname"`
}

var movies []Movie

func getMovies(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(movies) //讓 struct 解析成 Json
}

func deleteMovie(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	params := mux.Vars(r) //取得我們在 route 設定的 id
	for index, item := range movies {
		if item.ID == params["id"] {
			movies = append(movies[:index], movies[index+1:]...) //這邊的 movies[:index] 透過 slices 方法將目標 index 移除
			break
		}
	}
	json.NewEncoder(w).Encode((movies))
}

func getMovie(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	params := mux.Vars(r) //取得我們在 route 設定的 id
	for _, item := range movies {
		if item.ID == params["id"] {
			json.NewEncoder(w).Encode(item)
			return
		}
	}
}

func createMovie(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	var movie Movie
	_ = json.NewDecoder(r.Body).Decode(&movie)  //將 body內容解析成 movie?
	movie.ID = strconv.Itoa(rand.Intn(1000000)) //Itoa方法將 integer轉成 string、Intn方法產生亂數來製造出 id
	movies = append(movies, movie)
	json.NewEncoder(w).Encode(movie)
}

func updateMovie(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	params := mux.Vars(r)
	for index, item := range movies {
		if item.ID == params["id"] {
			movies = append(movies[:index], movies[index+1:]...) //這邊的 movies[:index] 透過 slices 方法將目標 index 移除
			var movie Movie
			_ = json.NewDecoder(r.Body).Decode(&movie)
			movie.ID = params["id"]
			movies = append(movies, movie)
			json.NewEncoder(w).Encode(movie)
			return
		}
	}
}

func main() {
	r := mux.NewRouter() //讓進來的 request 對應到正確的 router，並且會去 call 對應到的 route 的 handler

	//新增兩筆假資料進入 movies array
	movies = append(movies, Movie{ID: "1", Isbn: "438227", Title: "Emily in Paris", Director: &Director{Firstname: "Michael", Lastname: "Amodio"}})
	movies = append(movies, Movie{ID: "2", Isbn: "438228", Title: "The Silent Sea", Director: &Director{Firstname: "Hang-yong", Lastname: "Choi"}})

	r.HandleFunc("/movies", getMovies).Methods("GET") //實作 restful api
	r.HandleFunc("/movies/{id}", getMovie).Methods("GET")
	r.HandleFunc("/movies", createMovie).Methods("POST")
	r.HandleFunc("/movies/{id}", updateMovie).Methods("PUT")
	r.HandleFunc("/movies/{id}", deleteMovie).Methods("DELETE")

	fmt.Printf("Starting server at port 8000\n")
	//ListenAndServe第一個參數接收一個 type 是 string 的 addr, 第二個參數接收一個 type 是 Handler 的 handler 處理進來的 request
	log.Fatal(http.ListenAndServe(":8000", r))
	//log.Fatal會顯示log到終端機上，且執行 os.Exit(1) 终止程序的执行，后续的代码不会被执行。通常log.Fatal 用于记录关键错误或不可恢复的问题，以使程序停止运行
}
