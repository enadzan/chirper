USE [ChirpDb]
GO
INSERT [chirp_type] ([id], [name]) VALUES (1, N'Chirp')
INSERT [chirp_type] ([id], [name]) VALUES (3, N'Comment')
INSERT [chirp_type] ([id], [name]) VALUES (2, N'Re-chirp')
INSERT [reaction_type] ([id], [name], [is_positive], [is_negative]) VALUES (1, N'Like', 1, 0)
INSERT [reaction_type] ([id], [name], [is_positive], [is_negative]) VALUES (2, N'Dislike', 0, 1)
INSERT [reaction_type] ([id], [name], [is_positive], [is_negative]) VALUES (3, N'Heart', 1, 0)
INSERT [reaction_type] ([id], [name], [is_positive], [is_negative]) VALUES (4, N'Anger', 0, 1)
INSERT [reaction_type] ([id], [name], [is_positive], [is_negative]) VALUES (5, N'Confusion', 0, 0)

-- #Chirper2020!
INSERT INTO chirp_user (username, username_normalized, password, security_stamp)
VALUES ('chirper', 'CHIRPER', 'AQAAAAEAACcQAAAAEMiYdiqiuoMdYgd9kHwgwFMzOD3AIbi3Cu+CWSBlMDylA8sN3MOM8xIDvuaCntG95g==', '5UCGRTEOD2NBNLMZSPTLIBDJ7VHVN4E7');

