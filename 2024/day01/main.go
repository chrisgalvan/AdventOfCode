package main

import (
	"bufio"
	"fmt"
	"math"
	"os"
	"strings"

	"adventofcode/2024/utils"
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

		line1 = append(line1, utils.ToInt(values[0]))
		line2 = append(line2, utils.ToInt(values[1]))
	}

	One(line1, line2)
	Two(line1, line2)
}

func One(list1 []int, list2 []int) {

	utils.SortSlice(list1)
	utils.SortSlice(list2)

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
