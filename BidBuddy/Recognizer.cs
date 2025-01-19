using YoloDotNet;
using YoloDotNet.Enums;
using YoloDotNet.Models;
using SkiaSharp;

using Bidding;
using System.Runtime.CompilerServices;

namespace Recognizer {
    class Recognizer {
        // Initialize the YOLO model here
        private static Yolo yolo = new Yolo(new YoloOptions {
            OnnxModel = @"TODO",
            ModelType = ModelType.ObjectDetection,
            Cuda = false // Don't know if we have a GPU so better to ignore it
        });
        private static (int, Suit) ProcessLabel(string label) {
            Suit suit = label.Last() switch {
                'c' => Suit.Clubs,
                'd' => Suit.Diamonds,
                'h' => Suit.Hearts,
                's' => Suit.Spades,
                _ => throw new Exception()
            };
            int value = Array.IndexOf(new[] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"}, label.Substring(0, label.Length - 1));
            return (value, suit);
        }
        public static List<(int, Suit)> RecognizeHand(SKImage image) {
            List<ObjectDetection> results = yolo.RunObjectDetection(image); // Use default confidence and intersection-over-union values for now
            return results.Select(detection => ProcessLabel(detection.Label.Name)).ToList();
        }
    }
}