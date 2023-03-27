# Debug_Optimization

Unity의 Debug는 빌드 된 애플리케이션에도 동작 하므로 Debug를 많은 양을 사용하였을때 성능상 문제가 발생한다.
그 때문에 Debug클래스를 랩핑하여 빌드된 플리케이션에서는 Debug 클래스가 작동하지 않게 하기 위함이다.
추가로 Debug message에 컬러를 추가함으로 편의 기능을 추가한다.

본 코드에서 사용되는 Debug 기능은
1. Log
2. LogFormat
3. LogError
4. LogErrorFormat
5. LogWarning
6. LogWarningFormat

추가로 필요한 부분은 위를 참고하여 추가하여 사용할 수 있다.

====================================

UnityのDebugは、ビルドされた、アプリケーションにも、動作するため、Debugを大量に使用した場合、性能上の問題が発生する。
そのため、Debugクラスをラッピングして、ビルドされたアプリケーションでは、Debugクラスが動作しないようにするためだ。
追加で、Debug messageに、カラーを追加することで利便機能を追加する。

本コードで使用されるDebug機能は
1. Log
2. LogFormat
3. LogError
4. LogErrorFormat
5. LogWarning
6. LogWarningFormat

追加で、必要な部分は、上記を参考にして追加して使用することができる。
