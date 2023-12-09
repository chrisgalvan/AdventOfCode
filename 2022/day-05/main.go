package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	file, err := os.Open("input.txt")

	if err != nil {
		fmt.Println(err)
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	stacks := make(map[int][]rune)

	for scanner.Scan() {
		line := scanner.Text()

		if isCrate(line) {
			row := []rune(line)

			for i := 1; i < len(row); i += 4 {
				if row[i] != ' ' {
					n := (i-1)/4 + 1
					stacks[n] = append([]rune{row[i]}, stacks[n]...)
				}
			}
		}

		if isInstruction(line) {
			qty, from, to := defineMove(line)
			// fmt.Printf("qty %d from %d to %d\n", qty, from, to)

			for i := 0; i < qty; i++ {
				n := len(stacks[from]) - 1

				stacks[to] = append(stacks[to], stacks[from][n])
				stacks[from] = stacks[from][:n]
			}
		}
	}

	printResult(stacks)
}

func printResult(arr map[int][]rune) {
	fmt.Printf("\n")

	for i := 1; i <= len(arr); i++ {
		fmt.Printf("%c", arr[i][len(arr[i])-1])
	}

	// for i, row := range arr {
	// 	fmt.Printf("%d (%d): ", i, len(row))
	// 	for _, col := range row {
	// 		fmt.Printf(" %c |", col)
	// 	}
	// 	fmt.Printf("\n")
	// }
}

func isInstruction(input string) bool {
	return strings.Contains(input, "move")
}

func isCrate(input string) bool {
	return strings.Contains(input, "[")
}

func defineMove(input string) (int, int, int) {
	// tokens := strings.Fields(input)
	// qty := str_to_int(tokens[1])
	// from := str_to_int(tokens[3])
	// to := str_to_int(tokens[5])

	var qty, from, to int
	fmt.Sscanf(input, "move %d from %d to %d", &qty, &from, &to)

	// re := regexp.MustCompile(`\d+`)
	// result := re.FindAllString(input, -1)
	// return cint(result[0]), cint(result[1]), cint(result[2])

	return qty, from, to
}

func cint(input string) int {
	n, _ := strconv.Atoi(input)

	return n
}
