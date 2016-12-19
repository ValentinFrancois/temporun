import java.io.File;
import java.io.SequenceInputStream;
import javax.sound.sampled.AudioFileFormat;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;

public class WavConc {
    public static void main(String[] args) {
        String wavFile1 = "C:\\Users\\Romain\\workspace\\XMLParser\\audio\\M2.wav";
        String wavFile2 = "C:\\Users\\Romain\\workspace\\XMLParser\\audio\\P2.wav";

        try {
            AudioInputStream clip1 = AudioSystem.getAudioInputStream(new File(wavFile1));
            AudioInputStream clip2 = AudioSystem.getAudioInputStream(new File(wavFile2));

            AudioInputStream appendedFiles = 
                            new AudioInputStream(
                                new SequenceInputStream(clip1, clip2),     
                                clip1.getFormat(), 
                                clip1.getFrameLength() + clip2.getFrameLength());

            AudioSystem.write(appendedFiles, 
                            AudioFileFormat.Type.WAVE, 
                            new File("C:\\Users\\Romain\\workspace\\XMLParser\\audio\\wavAppended.wav"));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}