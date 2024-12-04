package main

import (
	"adventofcode/2024/utils"
	"fmt"
)

func main() {
	var matrix []string
	utils.StreamFile("input.txt", func(line string) {
		matrix = append(matrix, line)
	})

	one(matrix)
	two(matrix)
}

func one(lines []string) {
	total := 0
	for i, line := range lines {
		for k, char := range line {
			if char == 'X' {
				coordinates := getCoordinates(i, k, len(lines), len(line))
				for _, c := range coordinates {
					val := "X"
					for _, p := range c.point {
						val += string(lines[p.y][p.x])
					}

					if val == "XMAS" {
						total++
					}
				}
			}
		}
	}

	fmt.Printf("One - Result %d\n", total)
}

type coordinate struct {
	point []point
}

type point struct {
	x int
	y int
}

func getCoordinates(i int, k int, height int, length int) []coordinate {
	var c []coordinate

	if i >= 3 {
		// up
		c = append(c, coordinate{[]point{{x: k, y: i - 1}, {x: k, y: i - 2}, {x: k, y: i - 3}}})

		if k >= 3 {
			// left
			c = append(c, coordinate{[]point{{x: k - 1, y: i - 1}, {x: k - 2, y: i - 2}, {x: k - 3, y: i - 3}}})
		}

		if k+3 < length {
			// right
			c = append(c, coordinate{[]point{{x: k + 1, y: i - 1}, {x: k + 2, y: i - 2}, {x: k + 3, y: i - 3}}})
		}
	}

	if i+3 < height {
		// down
		c = append(c, coordinate{[]point{{x: k, y: i + 1}, {x: k, y: i + 2}, {x: k, y: i + 3}}})

		if k >= 3 {
			// left
			c = append(c, coordinate{[]point{{x: k - 1, y: i + 1}, {x: k - 2, y: i + 2}, {x: k - 3, y: i + 3}}})
		}

		if k+3 < length {
			// right
			c = append(c, coordinate{[]point{{x: k + 1, y: i + 1}, {x: k + 2, y: i + 2}, {x: k + 3, y: i + 3}}})
		}
	}

	if k+3 < length {
		// right
		c = append(c, coordinate{[]point{{x: k + 1, y: i}, {x: k + 2, y: i}, {x: k + 3, y: i}}})
	}

	if k >= 3 {
		// left
		c = append(c, coordinate{[]point{{x: k - 1, y: i}, {x: k - 2, y: i}, {x: k - 3, y: i}}})
	}

	return c
}

func two(lines []string) {
	total := 0
	for i, line := range lines {
		for k, char := range line {
			if char == 'A' {
				fmt.Printf("%d %d %d %d\n", i, k, len(lines), len(line))
				coordinates := getCoordinates2(i, k, len(lines), len(line))
				matches := 0
				for _, c := range coordinates {
					val := ""
					fmt.Println(c.point)
					for _, p := range c.point {
						val += string(lines[p.y][p.x])
					}

					if val == "MAS" {
						matches++
					}
				}

				if matches == 2 {
					total++
				}
			}
		}
	}

	fmt.Printf("Two - Result %d\n", total)
}

func getCoordinates2(i int, k int, height int, length int) []coordinate {
	var c []coordinate

	if i > 0 && i < height-1 && k > 0 && k < length-1 {
		// top left -> down right
		c = append(c, coordinate{[]point{{x: k - 1, y: i - 1}, {x: k, y: i}, {x: k + 1, y: i + 1}}})
		// down right -> top left
		c = append(c, coordinate{[]point{{x: k + 1, y: i + 1}, {x: k, y: i}, {x: k - 1, y: i - 1}}})
		// top right -> down left
		c = append(c, coordinate{[]point{{x: k + 1, y: i - 1}, {x: k, y: i}, {x: k - 1, y: i + 1}}})
		// down left -> top right
		c = append(c, coordinate{[]point{{x: k - 1, y: i + 1}, {x: k, y: i}, {x: k + 1, y: i - 1}}})
	}

	return c
}

// up left -1,-1, -2,-2, -3,-3,
// up 0,-1, 0,-2, 0,-3
// up right 1,-1, 2,-2, 3,-3
// right 1,0, 2,0 , 3,0,
// down right 1,1, 2,2 3,3
// down 0,1, 0,2, 0,3
// down left -1,1, -2,2, -3,3
// left -1,0, -2,0, -3,0
