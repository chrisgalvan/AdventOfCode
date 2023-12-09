package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

func main() {
	file, err := os.Open("input.txt")

	if err != nil {
		fmt.Println("Error opening file")
	}

	defer file.Close()

	scanner := bufio.NewScanner(file)

	const (
		Rock     = 0
		Paper    = 1
		Scissors = 2
	)

	const (
		Lose = 0
		Tie  = 1
		Win  = 2
	)

	var mapper = [3][3]int{
		// Lose, Tie, Win
		{Scissors, Rock, Paper}, // Rock
		{Rock, Paper, Scissors}, // Paper
		{Paper, Scissors, Rock}, // Scissors
	}

	my_map := map[string]int{
		"A": Rock,
		"B": Paper,
		"C": Scissors,
		"X": Lose,
		"Y": Tie,
		"Z": Win,
	}

	total := 0

	for scanner.Scan() {
		line := strings.Split(scanner.Text(), " ")

		var op_choice = my_map[line[0]]
		var result = my_map[line[1]]
		var my_choice = mapper[op_choice][result] + 1

		switch result {
		case Tie:
			total += 3
		case Lose:
			total += 0
		case Win:
			total += 6
		}

		total += my_choice
	}

	fmt.Println(total)
}
