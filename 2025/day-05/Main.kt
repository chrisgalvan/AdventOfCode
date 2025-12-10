import java.io.File

fun main() {
    val filePath = "./day-05/src/input.txt"

    val lines = File(filePath).readLines()
    val emptyLine = lines.indexOfFirst { it.isBlank() }

    val ranges = lines
        .subList(0, emptyLine)
        .map { line ->
            val (start, end) = line.split("-").map { it.toLong() }
            start..end
        }

    val numbers = lines
        .subList(emptyLine + 1, lines.size)
        .map { it.toLong() }

    partOne(ranges, numbers)
    partTwo(ranges)
}

fun partOne(ranges: List<LongRange>, numbers: List<Long>) {
//    var counter = 0
//
//    numbers.map { n ->
//        for (r in ranges) {
//            if (n >= r.first && n <= r.last) {
//                counter++
//                break
//            }
//        }
//    }

    val counter = numbers.count { n -> ranges.any { n in it } }

    println(counter)
}

fun partTwo(ranges: List<LongRange>) {
//    val sorted = ranges.sortedBy { it.first }
//    val merged = mutableListOf(sorted.first())
//
//    for (range in sorted) {
//        val last = merged.last()
//        if (range.first <= last.last + 1 ) {
//            merged[merged.lastIndex] = last.first..maxOf(range.last, last.last)
//        } else {
//            merged.add(range)
//        }
//    }

    val merged = ranges
        .sortedBy { it.first }
        .fold(mutableListOf<LongRange>()) { acc, range ->
            val last = acc.lastOrNull()
            if (last != null && range.first <= last.last + 1) {
                acc[acc.lastIndex] = last.first..maxOf(range.last, last.last)
            } else {
                acc.add(range)
            }
            acc
        }

    val counter = merged.sumOf { it.last - it.first + 1 }

    println(counter)
}