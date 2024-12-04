package utils

import (
	"bufio"
	"os"
	"sort"
	"strconv"
)

func ToInt(value string) int {
	i, _ := strconv.Atoi(value)

	return i
}

func SortSlice(arr []int) {
	sort.Slice(arr, func(i, j int) bool {
		return arr[i] < arr[j]
	})
}

type ProcessLineFunc func(string)

func StreamFile(filename string, processLine ProcessLineFunc) error {
	file, err := os.Open(filename)
	if err != nil {
		return err
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		processLine(scanner.Text())
	}

	if err := scanner.Err(); err != nil {
		return err
	}

	return nil
}
