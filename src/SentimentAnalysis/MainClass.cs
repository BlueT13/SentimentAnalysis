using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace SentimentAnalysis
{
	internal class MainClass
	{
		public static void Main(string[] args)
		{
			string fileLocation = "C:\\Users\\YongHo\\SentimentAnalysis\\첨부파일\\과제3\\";

			int count = 0;
			string[] negativeFileList = new string[900];
			string[] positiveFileList = new string[900];

			foreach (string line in File.ReadLines(fileLocation + @"negative_file_list.txt"))
			{
				negativeFileList[count] = line;
				count++;
			}

			count = 0;
			foreach (string line in File.ReadLines(fileLocation + @"positive_file_list.txt"))
			{
				positiveFileList[count] = line;
				count++;
			}

			string[] negativeReviews = new string[900];
			string[] positiveReviews = new string[900];

			for (int i = 0; i < negativeReviews.Length; i++)
			{
				negativeReviews[i] = File.ReadAllText(fileLocation + @"negative_reviews\" + negativeFileList[i]);
				negativeReviews[i] = Regex.Replace(negativeReviews[i], @"\d", "");
			}

			for (int i = 0; i < positiveReviews.Length; i++)
			{
				positiveReviews[i] = File.ReadAllText(fileLocation + @"positive_reviews\" + positiveFileList[i]);
				positiveReviews[i] = Regex.Replace(positiveReviews[i], @"\d", "");
			}

			Dictionary<string, int> negativeWords = new Dictionary<string, int>();
			string[] negativeWordsList = File.ReadAllLines(fileLocation + @"negative-words.txt");

			Dictionary<string, int> positiveWords = new Dictionary<string, int>();
			string[] positiveWordsList = File.ReadAllLines(fileLocation + @"positive-words.txt");

			foreach (string word in negativeWordsList)
			{
				negativeWords.Add(word, 0);
			}

			foreach (string word in positiveWordsList)
			{
				positiveWords.Add(word, 0);
			}

			// 나만의 부정 단어장 만들기
			for (int i = 0; i < negativeReviews.Length; i++)
			{
				string[] negativeReviewWords = negativeReviews[i].Split(
					new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')', '/', '-', '"', '*', '\n' },
					StringSplitOptions.RemoveEmptyEntries);

				for (int j = 0; j < negativeReviewWords.Length; j++)
				{
					if (negativeWords.ContainsKey(negativeReviewWords[j]))
					{
						negativeWords[negativeReviewWords[j]]++;
					}
				}
			}

			foreach (KeyValuePair<string, int> removeWords in negativeWords)
			{
				if (removeWords.Value == 0)
				{
					negativeWords.Remove(removeWords.Key);
				}
			}

			// 나만의 긍정 단어장 만들기
			for (int i = 0; i < positiveReviews.Length; i++)
			{
				string[] positiveReviewWords = positiveReviews[i].Split(
					new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')', '/', '-', '"', '*', '\n' },
					StringSplitOptions.RemoveEmptyEntries);

				for (int j = 0; j < positiveReviewWords.Length; j++)
				{
					if (positiveWords.ContainsKey(positiveReviewWords[j]))
					{
						positiveWords[positiveReviewWords[j]]++;
					}
				}
			}

			foreach (KeyValuePair<string, int> removeWords in positiveWords)
			{
				if (removeWords.Value == 0)
				{
					positiveWords.Remove(removeWords.Key);
				}
			}

			// 나만의 부정 단어장(positiveWords) 출력
			foreach (KeyValuePair<string, int> item in negativeWords)
			{
				Console.WriteLine(item.Key + " " + item.Value);
			}
			Console.WriteLine();

			// 나만의 긍정 단어장(positiveWords) 출력
			foreach (KeyValuePair<string, int> item in positiveWords)
			{
				Console.WriteLine(item.Key + " " + item.Value);
			}
			Console.WriteLine();

			// 나만의 부정, 긍정 단어장 텍스트 파일로 저장(negative-sentiment, positive-sentiment)
			string outputFilePath1 = fileLocation + "실제경과\\negative-sentiment.txt";
			string outputFilePath2 = fileLocation + "실제경과\\positive-sentiment.txt";

			using (StreamWriter writer = new StreamWriter(outputFilePath1))
			{
				foreach (KeyValuePair<string, int> item in negativeWords)
				{
					writer.WriteLine(item.Key + " " + item.Value);
				}
			}

			using (StreamWriter writer = new StreamWriter(outputFilePath2))
			{
				foreach (KeyValuePair<string, int> item in positiveWords)
				{
					writer.WriteLine(item.Key + " " + item.Value);
				}
			}

			// 나만의 백과사전을 기반으로 10개의 리뷰 파일을 positive, negative, don't know로 분류
			string[] result = new string[10];

			for (int i = 0; i < 10; i++)
			{
				string test = File.ReadAllText(fileLocation + @"test\" + string.Format("{0:00}", i + 1) + ".txt");
				string[] testWords = test.Split(
					new char[] { '.', '?', '!', ' ', ';', ':', ',', '(', ')', '/', '-', '"', '*', '\n' },
					StringSplitOptions.RemoveEmptyEntries);

				double negativeScore = 0;
				double positiveScore = 0;
				for (int j = 0; j < testWords.Length; j++)
				{
					// 부정 단어와 긍정 단어가 중복되면 딕셔너리에서 삭제
					if (negativeWords.ContainsKey(testWords[j]) == positiveWords.ContainsKey(testWords[j]))
					{
						negativeWords.Remove(testWords[j]);
						positiveWords.Remove(testWords[j]);
					}

					// 그리고 중복단어를 삭제한 딕셔너리로 개수 카운트
					if (negativeWords.ContainsKey(testWords[j]))
					{
						negativeScore += Math.Log(negativeWords[testWords[j]]);
					}
					if (positiveWords.ContainsKey(testWords[j]))
					{
						positiveScore += Math.Log(positiveWords[testWords[j]]);
					}
				}

				// positive, negative 분류 알고리즘(단어 빈도수에 로그를 넣은 값으로 계산)
				Console.WriteLine("{0} / {1}", negativeScore, positiveScore);
				if (negativeScore - positiveScore > 20)
				{
					result[i] = "negative";
				}
				else if (positiveScore - negativeScore > 20)
				{
					result[i] = "positive";
				}
				else
				{
					result[i] = "don't know";
				}
			}
			// 분류 결과를 result.txt에 저장
			File.WriteAllLinesAsync(fileLocation + "실제경과\\result.txt", result);
		}
	}
}
