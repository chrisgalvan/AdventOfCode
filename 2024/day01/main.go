package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"sort"
	"strconv"
	"strings"
)

func main() {
	file, err := os.Open("input.txt")
	if err != nil {
		panic(err)
	}

	defer file.Close()

	var line1 []int
	var line2 []int

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		line := scanner.Text()
		values := strings.Split(line, "   ")

		line1 = append(line1, toInt(values[0]))
		line2 = append(line2, toInt(values[1]))
	}

	One(line1, line2)
	Two(line1, line2)
}

func One(list1 []int, list2 []int) {

	sortSlice(list1)
	sortSlice(list2)

	var i = 0
	var j = len(list1)
	var total = 0.0

	for i < j {
		total += math.Abs(float64(list1[i]) - float64(list2[i]))
		i++
	}

	fmt.Printf("One - Result: %d\n", int(total))
}

func Two(list1 []int, list2 []int) {
	m := make(map[int]int)

	for _, value := range list2 {
		m[value]++
	}

	var total = 0
	for _, val := range list1 {
		total += val * m[val]
	}

	fmt.Printf("Two - Result: %d\n", total)
}

func toInt(value string) int {
	i, _ := strconv.Atoi(value)

	return i
}

func sortSlice(arr []int) {
	sort.Slice(arr, func(i, j int) bool {
		return arr[i] < arr[j]
	})
}
