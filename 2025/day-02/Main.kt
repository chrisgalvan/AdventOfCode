import java.io.File

fun main() {
    val filePath = "./day-02/src/input.txt"

    partOne(filePath)
}

fun partOne(filePath: String) {
    var counter: Long = 0
    File(filePath)
        .readLines()
        .forEach { line ->
            line.split(",")
                .forEach { seq ->
                    val (start, end) = seq.split("-").map { it.toLong() }

                    (start..end).forEach {n ->
                        val d = n.toString().length / 2
                        if (n.toString().length > 1 && n.toString().substring(0, d) == n.toString().substring(d)) {
                            counter += n
                        }
                    }
                }
        }
    println(counter)
}

fun partTwo(filePath: String) {

}