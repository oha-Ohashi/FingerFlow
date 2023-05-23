import torch
import torch.nn as nn
import torch.optim as optim
import numpy as np

# データセットの生成
def generate_dataset(num_samples):
    x = np.random.randint(0, 30, size=(num_samples, 100))
    y = np.zeros((num_samples, 100))
    for i in range(num_samples):
        y[i] = np.sort(x[i])
    return x, y

# ネットワークの定義
class Net(nn.Module):
    def __init__(self):
        super(Net, self).__init__()
        self.fc1 = nn.Linear(100, 50)
        self.fc2 = nn.Linear(50, 100)

    def forward(self, x):
        x = nn.functional.relu(self.fc1(x))
        x = self.fc2(x)
        return x

# データセットの生成
x_train, y_train = generate_dataset(100)
x_test, y_test = generate_dataset(10)

# モデルの定義
net = Net()

# デバイスの設定
device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
net.to(device)

# 損失関数と最適化アルゴリズムの定義
loss_fn = nn.MSELoss(reduction='mean')
optimizer = optim.SGD(net.parameters(), lr=0.001)

# モデルのトレーニング
for epoch in range(100):
    running_loss = 0.0
    for i in range(100):
        # データを取得
        inputs = torch.from_numpy(x_train[i]).float().unsqueeze(0)
        labels = torch.from_numpy(y_train[i]).float().unsqueeze(0)

        inputs = inputs.to(device)
        labels = labels.to(device)

        # 勾配を0に初期化
        optimizer.zero_grad()

        # 順伝播、損失の計算、逆伝播、パラメータの更新
        outputs = net(inputs)
        loss = loss_fn(outputs, labels)
        loss.backward()
        optimizer.step()

        running_loss += loss.item()

    print('[Epoch %d] loss: %.3f' % (epoch + 1, running_loss / 100))

# モデルの評価
test_loss = 0.0
for i in range(10):
    inputs = torch.from_numpy(x_test[i]).float().unsqueeze(0)
    labels = torch.from_numpy(y_test[i]).float().unsqueeze(0)

    inputs = inputs.to(device)
    labels = labels.to(device)

    outputs = net(inputs)
    loss = loss_fn(outputs, labels)

    test_loss += loss.item()

print('Test loss: %.3f' % (test_loss / 10))

# 予測の実行
inputs = torch.from_numpy(x_test[0]).float().unsqueeze(0)
inputs = inputs.to(device)
predicted_output = net(inputs)
predicted_output = predicted_output.squeeze().tolist()

print('Predicted output:', predicted_output)
