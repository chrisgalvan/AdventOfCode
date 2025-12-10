import java.io.File

fun main() {
    val filePath = "./day-04/src/input.txt"

    val matrix = File(filePath)
        .readLines()
        .map { it.toList()}

    partOne(matrix)
    partTwo(matrix)
}

fun partOne(matrix: List<List<Char>>) {
    var counter = 0
    matrix.forEachIndexed { i, row ->
        row.forEachIndexed { j, char ->
            if (matrix[i][j] == '@' && countNeighbors(matrix, i, j, emptySet()) < 4)
                counter++
        }
    }
    println(counter)
}

fun countNeighbors(matrix: List<List<Char>>, row: Int, col: Int, removed: Set<Pair<Int,Int>>): Int {
    val directions = listOf(-1 to -1, -1 to 0, -1 to 1, 0 to -1, 0 to 1, 1 to -1, 1 to 0, 1 to 1)

    return directions.count { (dr, dc) ->
        val pos = row + dr to col + dc
        matrix.getOrNull(row+dr)?.getOrNull(col+dc) == '@' && pos !in removed
    }
}

fun partTwo(matrix: List<List<Char>>) {
    val rollsRemoved = mutableSetOf<Pair<Int, Int>>()
    var total = 0

    while (true) {
        var rollsToBeRemoved = mutableSetOf<Pair<Int, Int>>()
        matrix.forEachIndexed { i, row ->
            row.forEachIndexed { j, char ->
                val pos = i to j
                if (pos !in rollsRemoved && char == '@' && countNeighbors(matrix, i, j, rollsRemoved) < 4) {
                    rollsToBeRemoved += pos
                }
            }
        }

        if (rollsToBeRemoved.isEmpty()) break

        total += rollsToBeRemoved.size
        rollsRemoved.addAll(rollsToBeRemoved)

        println(rollsToBeRemoved.size)
    }

    println(total)
}